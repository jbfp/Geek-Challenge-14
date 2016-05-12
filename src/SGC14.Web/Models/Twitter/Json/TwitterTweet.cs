using System;
using Newtonsoft.Json;
using SGC14.Web.Models.Twitter.Entities;

namespace SGC14.Web.Models.Twitter.Json
{
    public class TwitterTweet : IEquatable<TwitterTweet>
    {
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("entities")]
        public EntityCollection Entities { get; set; }

        [JsonProperty("favorite_count")]
        public int FavoriteCount { get; set; }

        [JsonProperty("id_str")]
        public string Id { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("possibly_sensitive")]
        public bool? PossiblySensitive { get; set; }

        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("user")]
        public TwitterUser User { get; set; }

        public bool Equals(TwitterTweet other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.Id, other.Id);
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

            return Equals((TwitterTweet) obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(TwitterTweet left, TwitterTweet right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TwitterTweet left, TwitterTweet right)
        {
            return !Equals(left, right);
        }
    }
}