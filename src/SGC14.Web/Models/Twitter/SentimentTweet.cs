using Nest;
using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models.Twitter
{
    [ElasticType(IdProperty = "Id", Name = "tweet")]
    public class SentimentTweet : Tweet
    {
        private readonly SentimentScore sentimentScore;

        public SentimentTweet(object id, string text, User user, DateTime? created, SentimentScore sentimentScore)
            : base(id, text, user, created)
        {
            this.sentimentScore = sentimentScore;
        }

        public SentimentTweet(Tweet tweet, SentimentScore sentiment)
            : this(tweet.Id, tweet.Text, tweet.User, tweet.Created, sentiment)
        {            
        }

        [ElasticProperty(OptOut = true)]
        [JsonProperty("sentiment")]
        public SentimentScore SentimentScore
        {
            get { return this.sentimentScore; }
        }
    }
}