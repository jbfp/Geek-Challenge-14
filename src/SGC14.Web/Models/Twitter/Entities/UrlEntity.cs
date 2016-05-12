using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models.Twitter.Entities
{
    public class UrlEntity : Entity, IEquatable<UrlEntity>
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public string ExpandedUrl { get; set; }

        public bool Equals(UrlEntity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(ExpandedUrl, other.ExpandedUrl);
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

            return Equals((UrlEntity) obj);
        }

        public override int GetHashCode()
        {
            return (ExpandedUrl != null ? ExpandedUrl.GetHashCode() : 0);
        }

        public static bool operator ==(UrlEntity left, UrlEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UrlEntity left, UrlEntity right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return ExpandedUrl ?? DisplayUrl ?? Url ?? base.ToString();
        }
    }
}