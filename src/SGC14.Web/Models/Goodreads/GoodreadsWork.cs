using System.Xml.Serialization;

namespace SGC14.Web.Models.Goodreads
{
    [XmlRoot("work")]
    public class GoodreadsWork
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("ratings_count")]
        public string RatingsCount { get; set; }

        [XmlElement("average_rating")]
        public string Rating { get; set; }

        [XmlElement("original_publication_day", IsNullable = true)]
        public string ReleaseDay { get; set; }

        [XmlElement("original_publication_month", IsNullable = true)]
        public string ReleaseMonth { get; set; }

        [XmlElement("original_publication_year", IsNullable = true)]
        public string ReleaseYear { get; set; }

        [XmlElement("image_url")]
        public string ImageUrl { get; set; }

        [XmlElement("best_book")]
        public BestBook Book { get; set; }

        [XmlRoot("best_book")]
        [XmlType("book")]
        public class BestBook
        {
            [XmlElement("id")]
            public int Id { get; set; }

            [XmlElement("title")]
            public string Title { get; set; }

            [XmlElement("author")]
            public Author BookAuthor { get; set; }

            [XmlRoot("author")]
            public class Author
            {
                [XmlElement("id")]
                public int Id { get; set; }

                [XmlElement("name")]
                public string Name { get; set; }
            }
        }
    }
}