using AutoMapper;
using SGC14.Web.Mapping.TypeConverters;
using SGC14.Web.Models;
using SGC14.Web.Models.Goodreads;

namespace SGC14.Web.Mapping.Profiles
{
    public class GoodreadsProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<GoodreadsWork, Book>().ConvertUsing<GoodreadsWorkBookTypeConverter>();
            base.Configure();
        }
    }
}