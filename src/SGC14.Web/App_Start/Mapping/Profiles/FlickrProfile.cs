using AutoMapper;
using SGC14.Web.Mapping.TypeConverters;
using SGC14.Web.Models;
using SGC14.Web.Models.Flickr;

namespace SGC14.Web.Mapping.Profiles
{
    public class FlickrProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<FlickrImage, Image>().ConvertUsing<FlickrImageTypeConverter>();
            base.Configure();
        }
    }
}