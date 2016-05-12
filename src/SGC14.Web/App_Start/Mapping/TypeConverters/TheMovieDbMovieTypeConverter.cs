using AutoMapper;
using SGC14.Web.Models;
using System;
using System.Globalization;
using SGC14.Web.Models.TheMovieDb;

namespace SGC14.Web.Mapping.TypeConverters
{
    internal class TheMovieDbMovieTypeConverter : ITypeConverter<TheMovieDbMovie, Movie>
    {
        public Movie Convert(ResolutionContext context)
        {
            var source = context.SourceValue as TheMovieDbMovie;

            if (source == null)
            {
                throw new ArgumentException("SourceValue is not TheMovieDbMovie.");
            }

            DateTimeOffset parsed;
            DateTime? created;

            if (DateTimeOffset.TryParseExact(source.ReleaseDate, "yyyy-MM-dd", null, DateTimeStyles.None, out parsed))
            {
                created = parsed.DateTime;
            }
            else
            {
                created = null;
            }

            return new Movie(source.Id, source.Title, source.VoteAverage, source.PosterPath, created);
        }
    }
}