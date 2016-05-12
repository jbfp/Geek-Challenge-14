using System;
using Microsoft.AspNet.Identity;
using NUnit.Framework;
using SGC14.Web.Models.Authentication;

namespace SGC14.Web.Tests.Models.Authentication
{
    [TestFixture]
    public class UserTests
    {
        private readonly Func<User> factory = () => new User(new Guid("425C3CEC-7B1E-41B0-951D-C48771F7AB52"), "userName");
            
        [Test]
        public void Constructor_requires_non_empty_id()
        {
            TestDelegate test = () => new User(Guid.Empty, "userName");
            var exception = Assert.Throws<ArgumentException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public void Constructor_requires_non_null_userName()
        {
            TestDelegate test = () => new User(Guid.NewGuid(), null);
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("userName"));
        }

        [Test]
        public void Implements_IUser_of_Guid()
        {
            var sut = this.factory();
            Assert.That(sut, Is.AssignableTo<IUser<Guid>>());
        }

        [Test]
        public void Implements_IEquatable_of_User()
        {
            var sut = this.factory();
            Assert.That(sut, Is.AssignableTo<IEquatable<User>>());
        }

        [Test]
        public void Instance_is_never_equal_to_null()
        {
            // Arrange.
            var sut = this.factory();

            // Act.
            var actual = sut.Equals(null);

            // Assert.
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Instances_are_equal_if_id_is_the_same()
        {
            // Arrange.
            var sut = this.factory();
            var other = this.factory();

            // Act.
            var actual = sut.Equals(other);

            // Assert.
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Instances_are_equal_if_they_are_the_same_reference()
        {
            // Arrange.
            var sut = this.factory();
            var other = sut;

            // Act.
            var actual = sut.Equals(other);

            // Assert.
            Assert.That(actual, Is.True);
        }

        [Test]
        public void GetHashCode_returns_hash_code_of_id()
        {
            // Arrange.
            var sut = this.factory();

            // Act.
            var actual = sut.GetHashCode();

            // Assert.
            Assert.That(actual, Is.EqualTo(sut.Id.GetHashCode()));
        }
    }
}