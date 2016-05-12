using AutoMapper;
using SGC14.Web.Models;
using System;
using SGC14.Web.Models.Flickr;

namespace SGC14.Web.Mapping.TypeConverters
{
    public class FlickrImageTypeConverter : ITypeConverter<FlickrImage, Image>
    {
        public Image Convert(ResolutionContext context)
        {
            var source = context.SourceValue as FlickrImage;

            if (source == null)
            {
                throw new ArgumentException("SourceValue is not FlickrImage.");
            }

            var id = source.Id;
            var url = string.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg", source.Farm, source.Server, source.Id, source.Secret);
            return new Image(id, null, url);
        }
    }
}