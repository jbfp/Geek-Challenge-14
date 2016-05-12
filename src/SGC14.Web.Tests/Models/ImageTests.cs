using NUnit.Framework;
using SGC14.Web.Models;
using System;

namespace SGC14.Web.Tests.Models
{
    [TestFixture]
    public class ImageTests
    {
        private readonly Func<Image> factory = () => new Image("id", DateTime.Now, "url");

        [Test]
        public void Implements_ISGC14Item()
        {
            var sut = this.factory();
            Assert.That(sut, Is.AssignableTo<ISGC14Item>());
        }

        [Test]
        public void Implements_IEquatable_of_Image()
        {
            var sut = this.factory();
            Assert.That(sut, Is.AssignableTo<IEquatable<Image>>());
        }

        [Test]
        public void Constructor_requires_non_null_id()
        {
            TestDelegate test = () => new Image(null, DateTime.Now, "url");
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public void Constructor_requires_non_null_url()
        {
            TestDelegate test = () => new Image("id", DateTime.Now, null);
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("url"));
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
            Assert.That(sut.Id, Is.EqualTo(other.Id));
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
            Assert.That(sut, Is.SameAs(other));
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Has_same_hash_code_as_id()
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