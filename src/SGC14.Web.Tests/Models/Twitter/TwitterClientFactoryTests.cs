using NUnit.Framework;
using SGC14.Web.Models.Twitter;
using System;

namespace SGC14.Web.Tests.Models.Twitter
{
    [TestFixture]
    public class TwitterClientFactoryTests
    {
        [Test]
        public void Create_requires_non_null_credentials()
        {
            // Arrange.
            var sut = new TwitterClientFactory();

            // Act.
            TestDelegate test = () => sut.Create(null);

            // Assert.
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("credentials"));
        }
    }
}