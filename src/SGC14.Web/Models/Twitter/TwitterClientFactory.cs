using System;

namespace SGC14.Web.Models.Twitter
{
    public class TwitterClientFactory : ITwitterClientFactory
    {
        public ITwitterClient Create(TwitterCredentials credentials)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }

            return new TwitterClient(credentials);
        }
    }
}