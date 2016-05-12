using System;
using System.Reactive.Linq;
using AutoMapper;
using SGC14.Web.Models.Twitter;
using SGC14.Web.Models.Twitter.Json;

namespace SGC14.Web.Helpers.Extensions
{
    internal static partial class ObservableExtensions
    {
        public static IObservable<Tweet> Map(this IObservable<TwitterTweet> tweets)
        {
            if (tweets == null)
            {
                throw new ArgumentNullException("tweets");
            }

            return tweets.Select(Mapper.Map<TwitterTweet, Tweet>);
        }

        public static IObservable<SentimentTweet> AnalyseSentiment(this IObservable<Tweet> tweets)
        {
            if (tweets == null)
            {
                throw new ArgumentNullException("tweets");
            }

            return tweets.Select(tweet => new SentimentTweet(tweet, SentimentAnalyser.GetSentimentScore(tweet.Text)));
        }
    }
}