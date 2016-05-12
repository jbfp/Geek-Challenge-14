using System;
using System.Collections.Generic;

namespace SGC14.Web.Helpers
{
    public struct Maybe<T> : IEquatable<Maybe<T>>, IEquatable<T>
    {
        private static readonly Maybe<T> none = new Maybe<T>();

        private readonly bool hasValue;
        private readonly T value;

        public Maybe(T value)
        {
            if (typeof (T).IsClass && value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.hasValue = true;
            this.value = value;
        }

        public T Value
        {
            get { return this.value; }
        }

        public bool HasValue
        {
            get { return this.hasValue; }
        }

        public static Maybe<T> None
        {
            get { return none; }
        }

        public bool Equals(Maybe<T> other)
        {
            return EqualityComparer<T>.Default.Equals(this.value, other.value);
        }

        public bool Equals(T other)
        {
            return Some(other).Equals(this);
        }

        public T GetValueOrDefault(T @default = default(T))
        {
            return HasValue ? Value : @default;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Maybe<T> && Equals((Maybe<T>) obj);
        }

        public override int GetHashCode()
        {
            return HasValue ? EqualityComparer<T>.Default.GetHashCode(this.value) : base.GetHashCode();
        }

        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return HasValue ? Value.ToString() : base.ToString();
        }

        public static implicit operator Maybe<T>(T value)
        {
            return Some(value);
        }

        public static Maybe<T> Some(T value)
        {
            return new Maybe<T>(value);
        }
    }
}