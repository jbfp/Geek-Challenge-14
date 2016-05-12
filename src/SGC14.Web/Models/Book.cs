using System;
using Newtonsoft.Json;

namespace SGC14.Web.Models
{
    public class Book : ISGC14Item, IEquatable<Book>
    {
        private readonly int id;
        private readonly int ratingsCount;
        private readonly double rating;
        private readonly string title;
        private readonly string author;
        private readonly string imageUrl;
        private readonly DateTime? created;

        public Book(int id, int ratingsCount, double rating, string title, string author, string imageUrl, DateTime? created)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            this.id = id;
            this.ratingsCount = ratingsCount;
            this.rating = rating;
            this.title = title;
            this.author = author;
            this.imageUrl = imageUrl;
            this.created = created;
        }

        [JsonProperty("id")]
        public object Id
        {
            get { return this.id; }
        }

        [JsonProperty("ratings_count")]
        public int RatingsCount
        {
            get { return this.ratingsCount; }
        }

        [JsonProperty("rating")]
        public double Rating
        {
            get { return this.rating; }
        }

        [JsonProperty("title")]
        public string Title
        {
            get { return this.title; }
        }

        [JsonProperty("author")]
        public string Author
        {
            get { return this.author; }
        }

        [JsonProperty("image_url")]
        public string ImageUrl
        {
            get { return this.imageUrl; }
        }

        [JsonProperty("created")]
        public DateTime? Created
        {
            get { return this.created; }
        }

        [JsonProperty("type")]
        public string Type
        {
            get { return "book"; }
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.id == other.id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Book) obj);
        }

        public override int GetHashCode()
        {
            return this.id;
        }

        public static bool operator ==(Book left, Book right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Book left, Book right)
        {
            return !Equals(left, right);
        }
    }
}