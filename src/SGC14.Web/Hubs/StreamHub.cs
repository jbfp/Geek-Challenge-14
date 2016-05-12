using Microsoft.AspNet.SignalR;
using SGC14.Web.Helpers.Extensions;
using SGC14.Web.Models;
using SGC14.Web.Models.Elasticsearch;
using SGC14.Web.Models.Parsers;
using SGC14.Web.Models.Twitter;
using System;
using System.Reactive.Linq;
using System.Threading;

namespace SGC14.Web.Hubs
{
    [Authorize]
    public class StreamHub : BaseTwitterHub
    {        
        public StreamHub(IDocumentParserFactory documentParserFactory, ITwitterClientFactory twitterClientFactory, IElasticClientFactory elasticClientFactory)
            : base(documentParserFactory, twitterClientFactory, elasticClientFactory)
        {
        }

        public void StartStream(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException("query");
            }

            var client = GetTwitterClient();
            var twitter = new Twitter(client);
            var elasticClient = GetElasticClient();
            var store = new ElasticsearchStore(elasticClient, query);

            ThreadPool.QueueUserWorkItem(state =>
            {
                var stream = twitter.Stream(query).Publish();
                var output = Observable.Merge<ISGC14Item>(
                    stream.Map().AnalyseSentiment(),
                    stream.MapMedia(),
                    stream.ExtractArticles(DocumentParserFactory));

                // Replace cancellation token (subscription.)
                var cts = Subscriptions.AddOrUpdate(Context.ConnectionId, _ => new CancellationTokenSource(), (k, v) =>
                {
                    using (v)
                    {
                        return new CancellationTokenSource();
                    }
                });

                // Add delegate to 'complete' stream when cancelled.
                cts.Token.Register(() => Clients.Caller.OnStreamCompleted());
                cts.Token.Register(() => Subscriptions.TryRemove(Context.ConnectionId, out cts));

                // Add caller's subscription.
                output.Sample(TimeSpan.FromMilliseconds(150))
                      .Subscribe(tweet => Clients.Caller.OnStreamNext(tweet),
                                 error => Clients.Caller.OnStreamError(error),
                                 () => Clients.Caller.OnStreamCompleted(),
                                 cts.Token);

                output.Buffer(TimeSpan.FromSeconds(1))
                      .Subscribe(store, cts.Token);

                // Start streaming.
                stream.Connect();
            });
        }

        public void StopStream()
        {
            CancelSubscription(Context.ConnectionId);
        }
    }
}