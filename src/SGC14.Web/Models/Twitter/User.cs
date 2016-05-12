using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models.Twitter
{
    public class User : IEquatable<User>
    {
        private readonly long id;
        private readonly string name;
        private readonly string screenName;
        private readonly string profileImageUrl;
        private readonly bool isVerified;

        public User(long id, string name, string screenName, string profileImageUrl, bool isVerified)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (screenName == null)
            {
                throw new ArgumentNullException("screenName");
            }

            if (profileImageUrl == null)
            {
                throw new ArgumentNullException("profileImageUrl");
            }

            this.id = id;
            this.name = name;
            this.screenName = screenName;
            this.profileImageUrl = profileImageUrl;
            this.isVerified = isVerified;
        }

        [JsonProperty("id")]
        public long Id
        {
            get { return this.id; }
        }

        [JsonProperty("name")]
        public string Name
        {
            get { return this.name; }
        }

        [JsonProperty("screen_name")]
        public string ScreenName
        {
            get { return this.screenName; }
        }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl
        {
            get { return this.profileImageUrl; }
        }

        [JsonProperty("is_verified")]
        public bool IsVerified
        {
            get { return this.isVerified; }
        }

        public bool Equals(User other)
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

            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}