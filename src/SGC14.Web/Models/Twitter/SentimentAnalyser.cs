using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace SGC14.Web.Models.Twitter
{
    public class SentimentAnalyser
    {
        private static readonly Lazy<IImmutableDictionary<string, int>> Words = new Lazy<IImmutableDictionary<string, int>>(() =>
        {
            string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string afinnPath = Path.Combine(applicationBase, "App_Data", "AFINN", "AFINN-111.txt");

            if (!File.Exists(afinnPath))
            {
                throw new FileNotFoundException("AFINN database does not exist.", afinnPath);
            }

            var lines = File.ReadAllLines(afinnPath);
            var words = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                // Each line is composed of the word and a value which are tab-separated.
                // Eg. abandoned	-2
                string[] values = line.Split('\t');
                string key = values[0].ToLower();
                int value = int.Parse(values[1]);
                words.Add(key, value);
            }

            return words.ToImmutableDictionary();
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static SentimentScore GetSentimentScore(string input)
        {
            var tokens = Tokenize(input);
            var sentimentScore = new SentimentScore(tokens);            

            foreach (var token in tokens)
            {
                if (!Words.Value.ContainsKey(token))
                {
                    continue;
                }

                var value = Words.Value[token];
                sentimentScore.AddWord(token, value);
            }

            return sentimentScore;
        }

        internal static IImmutableList<string> Tokenize(string input)
        {
            // Strip punctuation and superfluous spaces.
            input = RemovePunctuation(input);
            input = RemoveSuperfluousSpaces(input);
            input = input.ToLower();
            return input.Split(' ').ToImmutableList();
        }

        internal static string RemovePunctuation(string input)
        {
            return Regex.Replace(input, @"[^\w\s]", string.Empty);
        }

        internal static string RemoveSuperfluousSpaces(string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }
    }
}