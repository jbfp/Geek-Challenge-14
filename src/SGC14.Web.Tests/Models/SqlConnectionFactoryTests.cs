using NUnit.Framework;
using SGC14.Web.Models;
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace SGC14.Web.Tests.Models
{
    [TestFixture]
    public class SqlConnectionFactoryTests
    {
        private readonly Func<SqlConnectionFactory> factory =
            () => new SqlConnectionFactory(ConfigurationManager.ConnectionStrings["default"].ConnectionString);

        [Test]
        public void Constructor_requires_non_null_connectionString()
        {
            TestDelegate test = () => new SqlConnectionFactory(null);
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("connectionString"));
        }

        [Test]
        public async Task CreateConnectionAsync_creates_an_open_connection()
        {
            // Arrange.
            var sut = this.factory();

            // Act.
            var actual = await sut.CreateConnectionAsync();

            // Assert.            
            Assert.That(actual.State, Is.EqualTo(ConnectionState.Open));
        }
    }
}