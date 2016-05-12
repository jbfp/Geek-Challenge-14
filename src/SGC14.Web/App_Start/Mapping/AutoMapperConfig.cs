using AutoMapper;
using SGC14.Web.Mapping.Profiles;

namespace SGC14.Web.Mapping
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<TwitterProfile>();
                cfg.AddProfile<TheMovieDbProfile>();
                cfg.AddProfile<GoodreadsProfile>();
                cfg.AddProfile<FlickrProfile>();
            });
        }
    }

}