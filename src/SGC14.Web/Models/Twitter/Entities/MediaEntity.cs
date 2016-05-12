using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models.Twitter.Entities
{
    public class MediaEntity : UrlEntity, IEquatable<MediaEntity>
    {
        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("media_url")]
        public string MediaUrl { get; set; }

        [JsonProperty("media_url_https")]
        public string MediaUrlHttps { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public bool Equals(MediaEntity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return base.Equals(other) && Equals(Id, other.Id);
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

            return Equals((MediaEntity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ Id.GetHashCode();
            }
        }

        public static bool operator ==(MediaEntity left, MediaEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MediaEntity left, MediaEntity right)
        {
            return !Equals(left, right);
        }
    }
}