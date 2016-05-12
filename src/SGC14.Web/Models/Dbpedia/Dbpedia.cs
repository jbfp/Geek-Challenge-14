using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SGC14.Web.Models.Dbpedia
{
    public class Dbpedia : HttpWebContentProvider
    {
        public IObservable<DbpediaEntry> Search(string query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            // Sanitize query.
            query = Regex.Replace(query, @"[^\w\s]", string.Empty);
            query = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(query);
            query = Uri.EscapeDataString(query);

            var resourceUri = string.Format("http://dbpedia.org/resource/{0}", query);
            var requestUri = new Uri(string.Format("http://dbpedia.org/data/{0}.json", query));            
            var request = CreateRequest(requestUri);

            return Observable.StartAsync(ct => request.GetResponseAsync())
                             .Select(response => response.GetResponseStream())
                             .Where(stream => stream != null && stream.CanRead)
                             .SelectMany(stream => Observable.Using(() => new StreamReader(stream, Encoding.UTF8),
                                                                    reader => Observable.StartAsync(reader.ReadToEndAsync)))
                             .Select(JObject.Parse)
                             .Where(obj => obj.HasValues)
                             .Select(obj => new { resources = obj[resourceUri], abstracts = obj["http://dbpedia.org/ontology/abstract"] })
                             .Where(obj => obj.abstracts != null)
                             .Select(obj => obj.abstracts.FirstOrDefault(token => token.Value<string>("lang") == "en"))
                             .Select(@abstract => new DbpediaEntry(query, @abstract.Value<string>("value")))
                             .Catch(Observable.Empty<DbpediaEntry>());
        }
    }
}