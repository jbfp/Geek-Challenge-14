using System;
using Microsoft.AspNet.Identity;

namespace SGC14.Web.Models.Authentication
{
    public class User : IUser<Guid>, IEquatable<User>
    {
        private readonly Guid id;

        public User(string userName)
            : this(Guid.NewGuid(), userName)
        {
        }

        public User(Guid id, string userName)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id is empty.", "id");
            }

            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            this.id = id;
            this.UserName = userName;
        }

        public Guid Id
        {
            get { return this.id; }
        }

        public string UserName { get; set; }

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