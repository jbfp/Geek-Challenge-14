using System;
using System.Collections.Immutable;
using System.Linq;
using Newtonsoft.Json;

namespace SGC14.Web.Models.Twitter
{
    /// <summary>
    ///     https://github.com/Cheesebaron/SharpFinn/
    /// </summary>
    public class SentimentScore
    {
        private readonly IImmutableList<string> tokens;

        public SentimentScore(IImmutableList<string> tokens)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException("tokens");
            }

            if (tokens.Count == 0)
            {
                throw new ArgumentException("There are no tokens in the token list.", "tokens");
            }

            this.tokens = tokens;
            Words = ImmutableDictionary.Create<string, double>();
        }

        /// <summary>
        ///     Gets an immutable list of the tokens that were scored.
        /// </summary>
        [JsonIgnore]
        public IImmutableList<string> Tokens
        {
            get { return this.tokens; }
        }

        /// <summary>
        ///     Gets the total sentiment score of the words.
        /// </summary>
        [JsonProperty("score")]
        public double Score
        {
            get { return PositiveScore + NegativeScore; }
        }

        /// <summary>
        ///     Gets the total sentiment score of the positive words.
        /// </summary>
        [JsonProperty("positive_score")]
        public double PositiveScore
        {
            get { return Words.Where(kvp => kvp.Value > 0).Sum(kvp => kvp.Value); }
        }

        /// <summary>
        ///     Gets the total sentiment score of the negative words.
        /// </summary>
        [JsonProperty("negative_score")]
        public double NegativeScore
        {
            get { return Words.Where(kvp => kvp.Value < 0).Sum(kvp => kvp.Value); }
        }

        /// <summary>
        ///     Average sentiment score sentiment / # of tokens.
        /// </summary>
        [JsonProperty("average_score_tokens")]
        public double AverageScoreTokens
        {
            get { return Score / Tokens.Count; }
        }

        /// <summary>
        ///     Average sentiment score sentiment / # of words.
        /// </summary>
        [JsonProperty("average_score_words")]
        public double AverageScoreWords
        {
            get { return Score / Words.Count; }
        }

        /// <summary>
        ///     Gets an immutable dictionary of words + scores that were used from AFINN.
        /// </summary>
        [JsonIgnore]
        public IImmutableDictionary<string, double> Words { get; private set; }

        /// <summary>
        ///     Gets an immutable list of the words that had positive sentiment.
        /// </summary>
        [JsonProperty("positive")]
        public IImmutableList<string> Positive
        {
            get { return Words.Where(kvp => kvp.Value > 0).Select(kvp => kvp.Key).ToImmutableList(); }
        }

        /// <summary>
        ///     Gets an immutable list of the words that had negative sentiment.
        /// </summary>
        [JsonProperty("negative")]
        public IImmutableList<string> Negative
        {
            get { return Words.Where(kvp => kvp.Value < 0).Select(kvp => kvp.Key).ToImmutableList(); }
        }

        public void AddWord(string word, double sentimentValue)
        {
            Words = Words.Add(word, sentimentValue);
        }
    }
}