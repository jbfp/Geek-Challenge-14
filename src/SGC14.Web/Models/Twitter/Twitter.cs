using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SGC14.Web.Models.Twitter.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SGC14.Web.Models.Twitter
{
    public class Twitter
    {
        private const string SearchUri = "https://api.twitter.com/1.1/search/tweets.json";
        private const string FilterStreamUri = "https://stream.twitter.com/1.1/statuses/filter.json";
        private const string TrendsUri = "https://api.twitter.com/1.1/trends/place.json";

        private readonly ITwitterClient client;

        public Twitter(ITwitterClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            this.client = client;
        }

        public async Task<IImmutableList<TwitterTweet>> SearchAsync(string query, string language = "")
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            var parameters = new Dictionary<string, string>
            {
                { "q", query + " -RT" },
                { "lang", language },
                { "result_type", "popular" },
                { "count", "20" },
                { "include_entities", "true" }
            };

            var json = await this.client.RequestJsonAsync(SearchUri, parameters);

            if (json.HasValue == false)
            {
                return ImmutableList<TwitterTweet>.Empty;
            }

            var obj = JObject.Parse(json.Value);
            return obj["statuses"].Select(t => t.ToObject<TwitterTweet>())
                                  .ToImmutableList();
        }

        public IObservable<TwitterTweet> Stream(string query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            var parameters = new Dictionary<string, string>
            {
                { "track", query }
            };

            return this.client.OpenStreamAsync(FilterStreamUri, parameters)
                       .Select(JsonConvert.DeserializeObject<TwitterTweet>)
                       .Where(tweet => !tweet.Text.StartsWith("RT "));
        }

        public async Task<IImmutableList<string>> GetSuggestionsAsync()
        {
            var parameters = new Dictionary<string, string>
            {
                { "id", "1" }
            };

            var json = await this.client.RequestJsonAsync(TrendsUri, parameters);

            if (json.HasValue == false)
            {
                return ImmutableList<string>.Empty;
            }

            var array = JArray.Parse(json.Value);
            return array.SelectMany(obj => obj["trends"])
                        .Select(trend => trend["name"].Value<string>())
                        .ToImmutableList();
        }
    }
}