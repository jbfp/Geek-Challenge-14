using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models
{
    public class Image : ISGC14Item, IEquatable<Image>
    {
        private readonly DateTime? created;
        private readonly object id;
        private readonly string url;

        public Image(object id, DateTime? created, string url)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            this.id = id;
            this.url = url;
            this.created = created;
        }

        [JsonProperty("url")]
        public string Url
        {
            get { return this.url; }
        }

        public bool Equals(Image other)
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

        [JsonProperty("id")]
        public object Id
        {
            get { return this.id; }
        }

        [JsonProperty("type")]
        public string Type
        {
            get { return "image"; }
        }

        [JsonProperty("created")]
        public DateTime? Created
        {
            get { return this.created; }
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

            return Equals((Image) obj);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public static bool operator ==(Image left, Image right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Image left, Image right)
        {
            return !Equals(left, right);
        }
    }
}