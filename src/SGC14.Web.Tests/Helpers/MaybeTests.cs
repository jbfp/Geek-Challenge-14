using NUnit.Framework;
using SGC14.Web.Helpers;
using System;

namespace SGC14.Web.Tests.Helpers
{
    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void Constructor_requires_non_null_value_if_T_is_class()
        {
            TestDelegate test = () => new Maybe<object>(null);
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("value"));
        }

        [Test]
        public void Constructor_accepts_structs()
        {
            TestDelegate test = () => new Maybe<int>(0);
            Assert.That(test, Throws.Nothing);
        }

        [Test]
        public void Instances_are_equal_if_they_have_the_same_value()
        {
            // Arrange.
            const string value = "Maybe";
            var sut = new Maybe<string>(value);
            var other = new Maybe<string>(value);

            // Act.
            var actual = sut.Equals(other);

            // Assert.
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Instance_is_never_equal_to_null()
        {
            // Arrange.
            var sut = new Maybe<int>(0);

            // Act.
            var actual = sut.Equals(null);

            // Assert.
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Has_hash_code_of_value_when_value_exists()
        {
            // Arrange.
            const string value = "test";
            var sut = new Maybe<string>(value);            

            // Act.
            var actual = sut.GetHashCode();
            
            // Assert.
            Assert.That(actual, Is.EqualTo(value.GetHashCode()));
        }

        [Test]
        public void ToString_returns_string_representation_of_value()
        {
            // Arrange.
            const string value = "test";
            var expected = value.ToString();
            var sut = new Maybe<string>(value);

            // Act.
            var actual = sut.ToString();

            // Assert.
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToString_returns_regular_ToString_when_no_value()
        {
            // Arrange.
            var expected = typeof (Maybe<string>).ToString();
            var sut = new Maybe<string>();

            // Act.
            var actual = sut.ToString();

            // Assert.
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}