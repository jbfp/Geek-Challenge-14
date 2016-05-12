using System;

namespace SGC14.Web.Models.Authentication
{
    public class UserClaim : IEquatable<UserClaim>
    {
        private readonly Guid id;

        public UserClaim()
            : this(Guid.NewGuid())
        {            
        }

        public UserClaim(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id is empty.", "id");
            }

            this.id = id;
        }

        public Guid Id
        {
            get { return this.id; }
        }

        public Guid UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public bool Equals(UserClaim other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.id.Equals(other.id);
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

            return Equals((UserClaim) obj);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public static bool operator ==(UserClaim left, UserClaim right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserClaim left, UserClaim right)
        {
            return !Equals(left, right);
        }
    }
}