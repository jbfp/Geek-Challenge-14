using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models.Twitter.Entities
{
    public class UserMentionEntity : Entity, IEquatable<UserMentionEntity>
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public bool Equals(UserMentionEntity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(Id, other.Id);
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

            return Equals((UserMentionEntity) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(UserMentionEntity left, UserMentionEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserMentionEntity left, UserMentionEntity right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Name ?? ScreenName ?? Id.ToString();
        }
    }
}