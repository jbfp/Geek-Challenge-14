using AutoMapper;
using SGC14.Web.Models;
using SGC14.Web.Models.Twitter.Entities;
using System;

namespace SGC14.Web.Mapping.TypeConverters
{
    public class MediaEntityImageTypeConverter : ITypeConverter<MediaEntity, Image>
    {
        public Image Convert(ResolutionContext context)
        {
            var source = context.SourceValue as MediaEntity;

            if (source == null)
            {
                throw new ArgumentException("SourceValue is not MediaEntity.");
            }

            return new Image(source.Id, null, source.MediaUrlHttps);
        }
    }
}