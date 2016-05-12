using System;
using System.Reactive.Linq;
using AutoMapper;
using SGC14.Web.Models;
using SGC14.Web.Models.Twitter.Entities;
using SGC14.Web.Models.Twitter.Json;

namespace SGC14.Web.Helpers.Extensions
{
    internal static partial class ObservableExtensions
    {
        public static IObservable<Image> MapMedia(this IObservable<TwitterTweet> tweets)
        {
            if (tweets == null)
            {
                throw new ArgumentNullException("tweets");
            }

            return tweets.SelectMany(tweet => tweet.Entities.Media).Where(me => me.Type == "photo").Select(Mapper.Map<MediaEntity, Image>);
        }
    }
}