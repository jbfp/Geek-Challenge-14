using AutoMapper;
using Microsoft.AspNet.SignalR;
using SGC14.Web.Helpers.Extensions;
using SGC14.Web.Models;
using SGC14.Web.Models.Dbpedia;
using SGC14.Web.Models.Elasticsearch;
using SGC14.Web.Models.Flickr;
using SGC14.Web.Models.Goodreads;
using SGC14.Web.Models.Parsers;
using SGC14.Web.Models.TheMovieDb;
using SGC14.Web.Models.Twitter;
using SGC14.Web.Models.Twitter.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace SGC14.Web.Hubs
{
    [Authorize]
    public class SearchHub : BaseTwitterHub
    {
        private static readonly string TheMovieDbApiKey = ConfigurationManager.AppSettings["themoviedb_api_key"];
        private static readonly string GoodreadsApiKey = ConfigurationManager.AppSettings["goodreads_api_key"];
        private static readonly string FlickrApiKey = ConfigurationManager.AppSettings["flickr_api_key"];        

        public SearchHub(IDocumentParserFactory documentParserFactory, ITwitterClientFactory twitterClientFactory, IElasticClientFactory elasticClientFactory)
            : base(documentParserFactory, twitterClientFactory, elasticClientFactory)
        {
        }

        public void Search(string query, int page = 1, string language = "")
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException("query");
            }

            // Dispose previous subscriptions.
            CancellationTokenSource cts;
            if (Subscriptions.TryRemove(Context.ConnectionId, out cts))
            {
                Debug.WriteLine("{0} searching again.", (object) Context.ConnectionId);

                using (cts)
                {
                    cts.Cancel();
                }
            }

            var elasticClient = GetElasticClient();
            var store = new ElasticsearchStore(elasticClient, query);
            var twitterClient = GetTwitterClient();
            var twitter = new Twitter(twitterClient);

            ThreadPool.QueueUserWorkItem(async state =>
            {
                var twitterTweets = (await twitter.SearchAsync(query, language))
                    .ToObservable()
                    .Where(t => t.PossiblySensitive == false)
                    .Where(t => t.Text.StartsWith("RT ") == false)
                    .Distinct(t => t.Id)
                    .Catch(Observable.Empty<TwitterTweet>())
                    .SubscribeOn(new NewThreadScheduler())
                    .ObserveOn(new NewThreadScheduler());

                // Retrieve tweets from database, but don't if the query is not language-agnostic. We don't have a language filter.
                var cachedTweetsList = string.IsNullOrWhiteSpace(language) ? store.Search<Tweet>() : Enumerable.Empty<Tweet>();
                var cachedTweets = cachedTweetsList.ToObservable()
                                                   .SubscribeOn(new NewThreadScheduler());

                var tweets = Observable.Merge<ISGC14Item>(
                    twitterTweets.Map().Concat(cachedTweets).AnalyseSentiment(),
                    twitterTweets.MapMedia(),
                    twitterTweets.ExtractArticles(DocumentParserFactory))
                                       .SubscribeOn(new NewThreadScheduler());

                var movies = store.Search<Movie>().ToObservable().Concat(
                    new TheMovieDb(TheMovieDbApiKey).Get(query).ToObservable()
                                                    .Where(m => m.IsAdult == false)
                                                    .Distinct(m => m.Id)
                                                    .Select(Mapper.Map<TheMovieDbMovie, Movie>)
                                                    .Catch(Observable.Empty<Movie>()))
                                  .SubscribeOn(new NewThreadScheduler());

                var wiki = store.Search<DbpediaEntry>().ToObservable().Concat(
                    new Dbpedia().Search(query))
                    .SubscribeOn(new NewThreadScheduler());

                var images = store.Search<Image>().ToObservable().Concat(
                    new Flickr(FlickrApiKey).Search(query, page)
                                            .ToObservable()
                                            .Distinct(f => f.Id)
                                            .Select(Mapper.Map<FlickrImage, Image>)
                                            .Catch(Observable.Empty<Image>()))
                                  .SubscribeOn(new NewThreadScheduler());

                var books = store.Search<Book>().ToObservable().Concat(
                    new Goodreads(GoodreadsApiKey).Search(query, page)
                                                  .ToObservable()
                                                  .Distinct(w => w.Id)
                                                  .Select(Mapper.Map<GoodreadsWork, Book>)
                                                  .Catch(Observable.Empty<Book>()))
                                 .SubscribeOn(new NewThreadScheduler());

                var output = Observable.Merge(tweets, movies, wiki, images, books).Distinct(i => i.Id).Publish();

                // Get cancellation token.
                cts = Subscriptions.GetOrAdd(Context.ConnectionId, _ => new CancellationTokenSource());

                // Add delegate to 'complete' search when cancelled.
                cts.Token.Register(() => Clients.Caller.OnSearchCompleted());
                cts.Token.Register(() => Subscriptions.TryRemove(Context.ConnectionId, out cts));

                // Add caller's subscription.
                output.Subscribe(item => Clients.Caller.OnSearchNext(item),
                                 error => Clients.Caller.OnSearchError(error),
                                 () => Clients.Caller.OnSearchCompleted(),
                                 cts.Token);

                // Add database subscription.
                output.Subscribe(store, cts.Token);

                // Start searching.                
                output.Connect();
            });
        }
    }
}