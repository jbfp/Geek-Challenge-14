using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SGC14.Web.Models.TheMovieDb
{
    public class TheMovieDb : HttpWebContentProvider
    {
        private readonly string accessToken;

        public TheMovieDb(string accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException("accessToken");
            }

            this.accessToken = accessToken;
        }

        public IEnumerable<TheMovieDbMovie> Get(string query)
        {
            var parameters = new Dictionary<string, string>
            {
                { "include_adult", "false" },
                { "language", "en" },
                { "query", query },
                { "api_key", this.accessToken }
            };

            var requestUri = BuildUri("http://api.themoviedb.org/3/search/movie", parameters);
            var request = CreateRequest(requestUri);
            
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                if (stream == null || stream.CanRead == false)
                {
                    yield break;
                }

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var json = reader.ReadToEnd();
                    var obj = JObject.Parse(json);

                    foreach (var movie in obj["results"])
                    {
                        yield return movie.ToObject<TheMovieDbMovie>();
                    }
                }
            }
        }
    }
}