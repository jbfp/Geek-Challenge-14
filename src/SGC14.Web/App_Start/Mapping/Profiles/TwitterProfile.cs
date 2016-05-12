using AutoMapper;
using SGC14.Web.Mapping.TypeConverters;
using SGC14.Web.Models;
using SGC14.Web.Models.Twitter;
using SGC14.Web.Models.Twitter.Entities;
using SGC14.Web.Models.Twitter.Json;
using User = SGC14.Web.Models.Twitter.User;

namespace SGC14.Web.Mapping.Profiles
{
    internal class TwitterProfile : Profile
    {
        public override string ProfileName
        {
            get { return GetType().Name; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<TwitterUser, User>().ConvertUsing<TwitterUserTypeConverter>();
            Mapper.CreateMap<TwitterTweet, Tweet>().ConvertUsing<TwitterTweetTypeConverter>();
            Mapper.CreateMap<MediaEntity, Image>().ConvertUsing<MediaEntityImageTypeConverter>();
            base.Configure();
        }
    }
}