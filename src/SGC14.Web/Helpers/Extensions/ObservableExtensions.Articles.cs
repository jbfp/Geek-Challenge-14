using HtmlAgilityPack;
using SGC14.Web.Models;
using SGC14.Web.Models.Parsers;
using SGC14.Web.Models.Twitter.Json;
using System;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Xml.XPath;

namespace SGC14.Web.Helpers.Extensions
{
    internal static partial class ObservableExtensions
    {
        public static IObservable<Article> ExtractArticles(this IObservable<TwitterTweet> tweets, IDocumentParserFactory parserFactory)
        {
            return (from tweet in tweets
                    from urlEntity in tweet.Entities.Urls
                    let uri = new Uri(urlEntity.ExpandedUrl)
                    where Blacklist.IsValid(uri.Host)
                    select urlEntity).Distinct()
                                     .Select(urlEntity => urlEntity.ExpandedUrl)
                                     .Select(url => DownloadArticle(url, parserFactory))
                                     .Where(ma => ma.HasValue)
                                     .Select(ma => ma.Value)
                                     .ObserveOn(new NewThreadScheduler());
        }

        private static Maybe<Article> DownloadArticle(string url, IDocumentParserFactory parserFactory)
        {
            try
            {                
                var uri = new Uri(url);
                var request = WebRequest.CreateHttp(uri);
                request.UserAgent = "SGC14 1.0";

                using (var response = (HttpWebResponse) request.GetResponse())
                {                    
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream == null)
                        {
                            return Maybe<Article>.None;
                        }

                        Encoding encoding = string.IsNullOrEmpty(response.CharacterSet) ? Encoding.UTF8 : Encoding.GetEncoding(response.CharacterSet ?? "UTF-8");
                        var htmlDocument = new HtmlDocument();
                        htmlDocument.Load(stream, encoding);
                        XPathNavigator navigator = htmlDocument.CreateNavigator();
                        string host = uri.Host;
                        IDocumentParser parser = parserFactory.Get(host);
                        Maybe<Document> document = parser.Parse(navigator);

                        if (document.HasValue)
                        {
                            string title = document.Value.Title;
                            string description = document.Value.Description;
                            DateTime? created = document.Value.Created;
                            return Maybe<Article>.Some(new Article(url, title, description, created));
                        }
                    }
                }
            }
            catch
            {
                // TODO: Logging.
                // We don't really care about errors here. Maybe the internet is down, maybe the server is on fire.
                // We just wont let them buble up and disrupt the observable.
            }

            return Maybe<Article>.None;
        }
    }
}