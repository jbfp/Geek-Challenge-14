using Nest;
using NUnit.Framework;
using SGC14.Web.Models.Elasticsearch;

namespace SGC14.Web.Tests.Models.Elasticsearch
{
    [TestFixture]
    public class ElasticClientFactoryTests
    {
        [Test]
        public void Returns_IElasticClient()
        {
            // Arrange.
            var sut = new ElasticClientFactory();

            // Act.
            var actual = sut.Create(null, null);

            // Assert.
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Is.AssignableTo<IElasticClient>());
        }
    }
}