using AutoMapper;
using SGC14.Web.Models.Twitter;
using System;
using SGC14.Web.Models.Twitter.Json;

namespace SGC14.Web.Mapping.TypeConverters
{
    internal class TwitterUserTypeConverter : ITypeConverter<TwitterUser, User>
    {
        public User Convert(ResolutionContext context)
        {
            var source = context.SourceValue as TwitterUser;

            if (source == null)
            {
                throw new ArgumentException("SourceValue is not TwitterUser.");
            }

            var id = long.Parse(source.Id);
            var user = new User(id, source.Name, source.ScreenName, source.ProfileImageUrlHttps, source.IsVerified);
            return user;
        }
    }
}