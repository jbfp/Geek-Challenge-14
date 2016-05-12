using NUnit.Framework;
using SGC14.Web.Models;
using System;

namespace SGC14.Web.Tests.Models
{
    [TestFixture]
    public class ArticleTests
    {
        private const string validId = "id";
        private const string validTitle = "title";
        private const string validDescription = "description";
        private readonly Func<Article> factory = () => new Article(validId, validTitle, validDescription, DateTime.MinValue);

        [Test]
        public void Implements_ISGC14Item()
        {
            var sut = this.factory();
            Assert.That(sut, Is.AssignableTo<ISGC14Item>());
        }

        [Test]
        public void Implements_IEquatable_of_Article()
        {
            var sut = this.factory();
            Assert.That(sut, Is.AssignableTo<IEquatable<Article>>());
        }

        [Test]
        public void Constructor_requires_id()
        {
            TestDelegate test = () => new Article(null, validTitle, validDescription, DateTime.MinValue);
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public void Constructor_requires_title()
        {
            TestDelegate test = () => new Article(validId, null, validDescription, DateTime.MinValue);
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("title"));
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
        public void Instances_are_equal_if_they_are_the_same_reference()
        {
            // Arrange.
            var sut = this.factory();

            // Act.
            var actual = sut.Equals(sut);

            // Assert.
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Equal_instances_must_be_of_the_same_type()
        {
            // Arrange.
            var sut = this.factory();
            var other = new object();

            // Act.
            var actual = sut.Equals(other);

            // Assert.
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Has_same_hash_code_as_id()
        {
            // Arrange.
            var sut = this.factory();

            // Act.
            var actual = sut.GetHashCode();

            // Assert.
            Assert.That(actual, Is.EqualTo(validId.GetHashCode()));
        }

        [Test]
        public void Instances_with_the_same_id_are_equal()
        {
            // Arrange.
            var sut = this.factory();
            var other = this.factory();

            // Act.
            var actual = sut.Equals(other);

            // Assert.
            Assert.That(actual, Is.Not.SameAs(other));
            Assert.That(actual, Is.True);
        }
    }
}