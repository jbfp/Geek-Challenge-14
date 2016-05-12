using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using SGC14.Web.Hubs;
using SGC14.Web.Models;
using SGC14.Web.Models.Authentication;
using SGC14.Web.Models.Elasticsearch;
using SGC14.Web.Models.Parsers;
using System;
using System.Configuration;
using SGC14.Web.Models.Twitter;
using User = SGC14.Web.Models.Authentication.User;

namespace SGC14.Web
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            container.RegisterType<IDocumentParserFactory, DocumentParserFactory>()
                     .RegisterInstance<IAsyncDbConnectionFactory>(new SqlConnectionFactory(connectionString))
                     .RegisterType<IUserStore<User, Guid>, SqlUserStore>()
                     .RegisterType<ITwitterClientFactory, TwitterClientFactory>()
                     .RegisterType<IElasticClientFactory, ElasticClientFactory>()
                     .RegisterInstance<Func<UserManager<User, Guid>>>(() => new UserManager<User, Guid>(container.Resolve<IUserStore<User, Guid>>()))
                     .RegisterType<SearchHub>(new InjectionFactory(SearchHubFactory))
                     .RegisterType<StreamHub>(new InjectionFactory(StreamHubFactory));
        }

        private static SearchHub SearchHubFactory(IUnityContainer container)
        {
            var documentParserFactory = container.Resolve<IDocumentParserFactory>();
            var twitterClientFactory = container.Resolve<ITwitterClientFactory>();
            var elasticClientFactory = container.Resolve<IElasticClientFactory>();
            return new SearchHub(documentParserFactory, twitterClientFactory, elasticClientFactory);
        }

        private static StreamHub StreamHubFactory(IUnityContainer container)
        {
            var documentParserFactory = container.Resolve<IDocumentParserFactory>();
            var twitterClientFactory = container.Resolve<ITwitterClientFactory>();
            var elasticClientFactory = container.Resolve<IElasticClientFactory>();
            return new StreamHub(documentParserFactory, twitterClientFactory, elasticClientFactory);
        }
    }
}