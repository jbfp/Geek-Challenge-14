using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models
{
    public class Article : IEquatable<Article>, ISGC14Item
    {        
        private readonly object id;
        private readonly string title;
        private readonly string description;
        private readonly DateTime? created;

        public Article(object id, string title, string description, DateTime? created)
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
            this.description = description;
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
            get { return "article"; }
        }

        [JsonProperty("title")]       
        public string Title
        {
            get { return this.title; }
        }

        [JsonProperty("description")]
        public string Description
        {
            get { return this.description; }
        }

        [JsonProperty("created")]
        public DateTime? Created
        {
            get { return this.created; }
        }

        public bool Equals(Article other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.id, other.id);
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

            return Equals((Article) obj);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public static bool operator ==(Article left, Article right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Article left, Article right)
        {
            return !Equals(left, right);
        }
    }
}