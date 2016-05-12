using AutoMapper;
using SGC14.Web.Mapping.TypeConverters;
using SGC14.Web.Models;
using SGC14.Web.Models.TheMovieDb;

namespace SGC14.Web.Mapping.Profiles
{
    internal class TheMovieDbProfile : Profile
    {
        public override string ProfileName
        {
            get { return GetType().Name; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<TheMovieDbMovie, Movie>().ConvertUsing<TheMovieDbMovieTypeConverter>();
            base.Configure();
        }        
    }
}