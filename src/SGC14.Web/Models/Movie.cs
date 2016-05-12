using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models
{
    public class Movie : ISGC14Item, IEquatable<Movie>
    {
        private readonly DateTime? created;
        private readonly object id;
        private readonly string posterPath;
        private readonly double rating;
        private readonly string title;

        public Movie(object id, string title, double rating, string posterPath, DateTime? created)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            this.id = id;
            this.title = title;
            this.rating = rating;
            this.posterPath = posterPath;
            this.created = created;
        }

        [JsonProperty("id")]
        public object Id
        {
            get { return this.id; }
        }

        [JsonProperty("type")]
        public string Type
        {
            get { return "movie"; }
        }

        [JsonProperty("title")]
        public string Title
        {
            get { return this.title; }
        }

        [JsonProperty("rating")]
        public double Rating
        {
            get { return this.rating; }
        }

        [JsonProperty("poster_path")]
        public string PosterPath
        {
            get { return this.posterPath; }
        }

        [JsonProperty("created")]
        public DateTime? Created
        {
            get { return this.created; }
        }

        public bool Equals(Movie other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(this.id, other.id);
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

            return Equals((Movie) obj);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public static bool operator ==(Movie left, Movie right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Movie left, Movie right)
        {
            return !Equals(left, right);
        }
    }
}