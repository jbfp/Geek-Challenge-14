using NUnit.Framework;
using SGC14.Web.Helpers;

namespace SGC14.Web.Tests.Helpers
{
    [TestFixture]
    public class BlacklistTests
    {
        [Test]
        [TestCase("instagram.com", false)]
        [TestCase("instagram.com/p/uEQwaJmz46/", false)]
        public void IsValid_returns_false_on_whitelisted_urls(string url, bool expected)
        {
            // Act.
            bool actual = Blacklist.IsValid(url);

            // Assert.
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}