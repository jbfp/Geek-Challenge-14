using System.Globalization;
using AutoMapper;
using SGC14.Web.Models;
using System;
using SGC14.Web.Models.Goodreads;

namespace SGC14.Web.Mapping.TypeConverters
{
    public class GoodreadsWorkBookTypeConverter : ITypeConverter<GoodreadsWork, Book>
    {
        private static readonly CultureInfo American = CultureInfo.GetCultureInfo("en-US");

        public Book Convert(ResolutionContext context)
        {
            var source = context.SourceValue as GoodreadsWork;

            if (source == null)
            {
                throw new ArgumentException("SourceValue is not GoodreadsBook.");
            }

            int ratingsCount;
            int.TryParse(source.RatingsCount, out ratingsCount);
            double rating;
            double.TryParse(source.Rating, NumberStyles.Float, American, out rating);            
            var imageUrl = source.ImageUrl;
            var book = source.Book;

            if (book == null)
            {
                throw new ArgumentException("Work is not a book.");
            }

            var id = book.Id;
            var title = book.Title;
            var author = book.BookAuthor;
            var name = string.Empty;

            if (author != null)
            {
                name = author.Name;
            }

            // Publication date can actually be null.
            DateTime? created = null;
            if (source.ReleaseYear != null)
            {
                int year;
                if (int.TryParse(source.ReleaseYear, out year) && year > 0)
                {
                    int month;
                    int day;

                    if (!int.TryParse(source.ReleaseMonth, out month))
                    {
                        month = 1;
                    }

                    if (!int.TryParse(source.ReleaseDay, out day))
                    {
                        day = 1;
                    }

                    created = new DateTime(year,
                                           month,
                                           day,
                                           0, 0, 0);
                }
            }

            return new Book(id, ratingsCount, rating, title, name, imageUrl, created);
        }
    }
}