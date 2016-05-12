using NUnit.Framework;
using SGC14.Web.Models.Parsers;
using System;

namespace SGC14.Web.Tests.Models.Parsers
{
    [TestFixture]
    public class DocumentParserFactoryTests
    {
        [Test]
        public void Get_requires_host_argument()
        {
            // Arrange.
            var sut = new DocumentParserFactory();

            // Act.
            TestDelegate test = () => sut.Get(null);

            // Assert.
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("host"));
        }

        [Test]
        [TestCase("www.bbc.com", typeof(BbcParser))]
        [TestCase("bbc.com", typeof(BbcParser))]
        [TestCase("test.articles.bbc.in", typeof(BbcParser))]
        [TestCase("abbc.com", typeof(DefaultParser))]
        public void Get_ignores_subdomains_when_selecting_parser(string host, Type expectedParserType)
        {
            // Arrange.
            var sut = new DocumentParserFactory();

            // Act.
            IDocumentParser actual = sut.Get(host);

            // Assert.
            Assert.That(actual, Is.InstanceOf(expectedParserType));
        }

        [Test]
        public void Get_returns_DefaultParser_for_unknown_host()
        {
            // Arrange.
            var sut = new DocumentParserFactory();

            // Act.
            IDocumentParser parser = sut.Get("random.org");

            // Assert
            Assert.That(parser, Is.InstanceOf<DefaultParser>());
        }
    }
}