using AutoMapper;
using SGC14.Web.Models.Twitter;
using System;
using System.Globalization;
using SGC14.Web.Models.Twitter.Json;

namespace SGC14.Web.Mapping.TypeConverters
{
    internal class TwitterTweetTypeConverter : ITypeConverter<TwitterTweet, Tweet>
    {
        public Tweet Convert(ResolutionContext context)
        {
            var value = context.SourceValue as TwitterTweet;

            if (value == null)
            {
                throw new ArgumentException("SourceValue is not TwitterTweet.");
            }

            DateTimeOffset createdAt;
            DateTime? created;

            if (DateTimeOffset.TryParseExact(value.CreatedAt, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out createdAt))
            {
                created = createdAt.DateTime;
            }
            else
            {
                created = null;
            }

            var user = context.Engine.Map<TwitterUser, User>(value.User);
            var tweet = new Tweet(value.Id, value.Text, user, created);
            return tweet;
        }
    }
}