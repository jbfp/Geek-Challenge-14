using System.Configuration;
using System.Security.Claims;

namespace SGC14.Web.Models.Twitter
{
    public class TwitterCredentials
    {
        private readonly string consumerKey;
        private readonly string consumerSecret;
        private readonly string accessToken;
        private readonly string accessTokenSecret;

        public TwitterCredentials(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.accessToken = accessToken;
            this.accessTokenSecret = accessTokenSecret;
        }

        public string ConsumerKey
        {
            get { return this.consumerKey; }
        }

        public string ConsumerSecret
        {
            get { return this.consumerSecret; }
        }

        public string AccessToken
        {
            get { return this.accessToken; }
        }

        public string AccessTokenSecret
        {
            get { return this.accessTokenSecret; }
        }

        public static TwitterCredentials GetFromClaimsIdentity(ClaimsIdentity identity)
        {
            // TODO: ConfigurationManager does not belong here though.
            var consumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
            var accessToken = identity.FindFirst(c => c.Type == TwitterClaimTypes.AccessToken).Value;
            var accessTokenSecret = identity.FindFirst(c => c.Type == TwitterClaimTypes.AccessTokenSecret).Value;
            return new TwitterCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }
    }
}