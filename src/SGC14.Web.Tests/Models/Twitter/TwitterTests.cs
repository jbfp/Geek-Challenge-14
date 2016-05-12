using Moq;
using NUnit.Framework;
using SGC14.Web.Helpers;
using SGC14.Web.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGC14.Web.Tests.Models.Twitter
{
    [TestFixture]
    public class TwitterTests
    {
        private Mock<ITwitterClient> client;

        [SetUp]
        public void SetUp()
        {
            client = new Mock<ITwitterClient>();
        }

        [Test]
        public void Constructor_requires_non_null_credentials()
        {
            TestDelegate test = () => new Web.Models.Twitter.Twitter(null);
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("credentials"));
        }

        [Test]
        public void SearchAsync_requires_non_null_query()
        {
            // Arrange.            
            var sut = new Web.Models.Twitter.Twitter(client.Object);

            // Act.
            TestDelegate test = async () => await sut.SearchAsync(null);

            // Assert.
            var exception = Assert.Throws<ArgumentNullException>(test);
            Assert.That(exception.ParamName, Is.EqualTo("query"));
        }

        [Test]
        public async Task SearchAsync_returns_empty_list_if_client_fails()
        {
            // Arrange.
            client.Setup(c => c.RequestJsonAsync(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                  .ReturnsAsync(Maybe<string>.None);
            var sut = new Web.Models.Twitter.Twitter(client.Object);

            // Act.
            var actual = await sut.SearchAsync("q");

            // Assert.
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public async Task SearchAsync_gets_correct_number_of_statuses()
        {
            // Arrange.
            const string json = "{\"statuses\": [{}, {}]}";
            client.Setup(c => c.RequestJsonAsync(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                  .ReturnsAsync(Maybe<string>.Some(json));
            var sut = new Web.Models.Twitter.Twitter(client.Object);

            // Act.
            var actual = await sut.SearchAsync("q");

            // Assert.
            Assert.That(actual, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task SearchAsync_returns_empty_list_if_no_statuses()
        {
            // Arrange.
            const string json = "{\"statuses\": []}";
            client.Setup(c => c.RequestJsonAsync(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                  .ReturnsAsync(Maybe<string>.Some(json));
            var sut = new Web.Models.Twitter.Twitter(client.Object);

            // Act.
            var actual = await sut.SearchAsync("q");

            // Assert.
            Assert.That(actual, Has.Count.EqualTo(0));
        }

        [Test]
        public async Task GetSuggestionsAsync_returns_empty_list_if_client_fails()
        {
            // Arrange.
            client.Setup(c => c.RequestJsonAsync(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                  .ReturnsAsync(Maybe<string>.None);
            var sut = new Web.Models.Twitter.Twitter(client.Object);

            // Act.
            var actual = await sut.SearchAsync("q");

            // Assert.
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public async Task GetSuggestionsAsync_gets_correct_number_of_suggestions()
        {
            // Arrange.
            const string json = "[{\"trends\": [{\"name\": \"test\"}, {\"name\": \"test2\"}]}]";
            client.Setup(c => c.RequestJsonAsync(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                  .ReturnsAsync(Maybe<string>.Some(json));
            var sut = new Web.Models.Twitter.Twitter(client.Object);

            // Act.
            var actual = await sut.GetSuggestionsAsync();

            // Assert.
            Assert.That(actual, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetSuggestionsAsync_returns_empty_list_if_no_statuses()
        {
            // Arrange.
            const string json = "[{\"trends\": []}]";
            client.Setup(c => c.RequestJsonAsync(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                  .ReturnsAsync(Maybe<string>.Some(json));
            var sut = new Web.Models.Twitter.Twitter(client.Object);

            // Act.
            var actual = await sut.GetSuggestionsAsync();

            // Assert.
            Assert.That(actual, Has.Count.EqualTo(0));
        }
    }
}