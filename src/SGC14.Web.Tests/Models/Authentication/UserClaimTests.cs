using System;
using NUnit.Framework;
using SGC14.Web.Models.Authentication;

namespace SGC14.Web.Tests.Models.Authentication
{
    [TestFixture]
    public class UserClaimTests
    {
        private readonly Func<UserClaim> factory = () => new UserClaim(new Guid("1369D489-F20C-40FB-8B94-1B415CED7CAD"));

        [Test]
        public void Implements_IEquatable_of_UserClaim()
        {
            var sut = this.factory();
            Assert.That(sut, Is.AssignableTo<IEquatable<UserClaim>>());
        }

        [Test]
        public void Constructor_requires_non_empty_id()
        {
            TestDelegate test = () => new UserClaim(Guid.Empty);
            var exception = Assert.Throws<ArgumentException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("id"));
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
        public void Instances_are_equal_when_id_is_same()
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
    }
}