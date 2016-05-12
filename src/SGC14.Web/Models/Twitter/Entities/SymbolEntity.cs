using Newtonsoft.Json;
using System;

namespace SGC14.Web.Models.Twitter.Entities
{
    public class SymbolEntity : Entity, IEquatable<SymbolEntity>
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        public bool Equals(SymbolEntity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Text, other.Text);
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

            return Equals((SymbolEntity) obj);
        }

        public override int GetHashCode()
        {
            return (Text != null ? Text.GetHashCode() : 0);
        }

        public static bool operator ==(SymbolEntity left, SymbolEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SymbolEntity left, SymbolEntity right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Text ?? base.ToString();
        }
    }
}