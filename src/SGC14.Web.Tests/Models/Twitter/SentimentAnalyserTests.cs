using NUnit.Framework;
using SGC14.Web.Models.Twitter;
using System.Collections.Immutable;

namespace SGC14.Web.Tests.Models.Twitter
{
    [TestFixture]
    public class SentimentAnalyserTests
    {
        [Test]
        [TestCase("I had a nice day with friends.", 3.0)]
        [TestCase("I have nothing bad to say about LG.", -3.0)]
        [TestCase("Baboons are totally ridiculous looking with their ugly red bottoms", -6.0)]
        public void GetScore_calculates_correct_score(string input, double score)
        {
            // Act.
            var sentimentScore = SentimentAnalyser.GetSentimentScore(input);
            var actual = sentimentScore.Score;

            // Assert.
            Assert.That(actual, Is.EqualTo(score));
        }

        [Test]
        public void Tokenize_tokenizes_string()
        {
            // Arrange.
            const string input = "I had a nice day - however, it was quite rainy!";
            string[] expected = { "i", "had", "a", "nice", "day", "however", "it", "was", "quite", "rainy" };

            // Act.
            IImmutableList<string> actual = SentimentAnalyser.Tokenize(input);

            // Assert.
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void RemovePunctuation_removes_punctuation()
        {
            // Arrange.
            const string input = "I didn't have a nice day - however, it was quite rainy!";
            const string expected = "I didnt have a nice day  however it was quite rainy";

            // Act.
            string actual = SentimentAnalyser.RemovePunctuation(input);

            // Assert.
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void RemoveSuperfluousSpaces_removes_superfluous_spaces()
        {
            // Arrange.
            const string input = "I      need  these  spaces";
            const string expected = "I need these spaces";

            // Act.
            var actual = SentimentAnalyser.RemoveSuperfluousSpaces(input);

            // Assert.
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}