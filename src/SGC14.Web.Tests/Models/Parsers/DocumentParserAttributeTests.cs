using System;
using NUnit.Framework;
using SGC14.Web.Models.Parsers;

namespace SGC14.Web.Tests.Models.Parsers
{
    [TestFixture]
    public class DocumentParserAttributeTests
    {
        [Test]
        public void Constructor_requires_non_null_host()
        {
            TestDelegate test = () => new DocumentParserAttribute(null);
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("host"));
        }
    }
}