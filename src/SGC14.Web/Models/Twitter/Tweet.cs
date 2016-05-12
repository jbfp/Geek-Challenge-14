using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models.Twitter
{
    public class Tweet : ISGC14Item, IEquatable<Tweet>
    {
        private readonly object id;
        private readonly string text;
        private readonly User user;
        private readonly DateTime? created;

        public Tweet(object id, string text, User user, DateTime? created)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.id = id;
            this.text = text;
            this.user = user;
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
            get { return "tweet"; }
        }

        [JsonProperty("text")]
        public string Text
        {
            get { return this.text; }
        }

        [JsonProperty("user")]
        public User User
        {
            get { return this.user; }
        }

        [JsonProperty("created")]
        public DateTime? Created
        {
            get { return this.created; }
        }

        public bool Equals(Tweet other)
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

            return Equals((Tweet) obj);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public static bool operator ==(Tweet left, Tweet right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tweet left, Tweet right)
        {
            return !Equals(left, right);
        }
    }

}