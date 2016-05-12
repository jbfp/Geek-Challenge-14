using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SGC14.Web.Models.Flickr
{
    public class Flickr : HttpWebContentProvider
    {
        private readonly string key;

        public Flickr(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            this.key = key;
        }

        public IEnumerable<FlickrImage> Search(string query, int page = 1)
        {
            var parameters = new Dictionary<string, string>
            {
                { "method", "flickr.photos.search" },
                { "api_key", this.key },
                { "text", query },
                { "sort", "relevance" },
                { "content_type", "1" },
                { "media", "photos" },
                { "license", "1,2,3,4,5,6,7,8" },
                { "format", "json" },
                { "page", page.ToString() },
                { "per_page", "15" }
            };

            var requestUri = BuildUri("https://api.flickr.com/services/rest/", parameters);
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

                    // Sanitize JSON, remove 'jsonFlickrApi(...)'.
                    json = json.Remove(0, "jsonFlickrApi(".Length);
                    json = json.Remove(json.Length - 1, 1);
                    
                    // Parse JSON.
                    var obj = JObject.Parse(json);

                    foreach (var photo in obj["photos"]["photo"])
                    {
                        yield return photo.ToObject<FlickrImage>();
                    }
                }
            }
        }
    }
}