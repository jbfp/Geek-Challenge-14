using System.Configuration;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Nest;
using SGC14.Web.Models.Elasticsearch;
using SGC14.Web.Models.Parsers;
using SGC14.Web.Models.Twitter;
using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SGC14.Web.Hubs
{
    public abstract class BaseTwitterHub : Hub
    {
        private static readonly Uri ElasticsearchStoreUri = new Uri(ConfigurationManager.AppSettings["elasticsearch_url"]);
        private static readonly ConcurrentDictionary<string, CancellationTokenSource> subscriptions = new ConcurrentDictionary<string, CancellationTokenSource>();        
        private readonly IDocumentParserFactory documentParserFactory;
        private readonly ITwitterClientFactory twitterClientFactory;
        private readonly IElasticClientFactory elasticClientFactory;

        protected BaseTwitterHub(IDocumentParserFactory documentParserFactory, ITwitterClientFactory twitterClientFactory, IElasticClientFactory elasticClientFactory)
        {
            if (documentParserFactory == null)
            {
                throw new ArgumentNullException("documentParserFactory");
            }

            if (twitterClientFactory == null)
            {
                throw new ArgumentNullException("twitterClientFactory");
            }

            if (elasticClientFactory == null)
            {
                throw new ArgumentNullException("elasticClientFactory");
            }

            this.documentParserFactory = documentParserFactory;
            this.twitterClientFactory = twitterClientFactory;
            this.elasticClientFactory = elasticClientFactory;
        }

        protected static ConcurrentDictionary<string, CancellationTokenSource> Subscriptions
        {
            get { return subscriptions; }
        }

        protected IDocumentParserFactory DocumentParserFactory
        {
            get { return this.documentParserFactory; }
        }

        protected IElasticClient GetElasticClient()
        {
            return this.elasticClientFactory.Create(ElasticsearchStoreUri, "sgc14");
        }

        protected ITwitterClient GetTwitterClient()
        {
            var credentials = GetTwitterCredentials();
            return this.twitterClientFactory.Create(credentials);
        }

        private TwitterCredentials GetTwitterCredentials()
        {
            var claimsIdentity = Context.User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
            {
                throw new NotAuthorizedException("User identity has no claims.");
            }

            return TwitterCredentials.GetFromClaimsIdentity(claimsIdentity);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            CancelSubscription(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        protected void CancelSubscription(string connectionId)
        {
            // Dispose all subscriptions for connection, stopping searches and streams.                   
            CancellationTokenSource cts;
            if (Subscriptions.TryRemove(connectionId, out cts))
            {
                using (cts)
                {
                    cts.Cancel();
                }
            }
        }
    }
}