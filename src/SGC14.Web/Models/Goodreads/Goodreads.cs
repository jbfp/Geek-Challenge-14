using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SGC14.Web.Models.Goodreads
{
    public class Goodreads : HttpWebContentProvider
    {
        private readonly string key;

        public Goodreads(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            this.key = key;
        }

        public IEnumerable<GoodreadsWork> Search(string query, int page = 1)
        {
            // Sanitize query.
            query = Regex.Replace(query, @"[^\w\s]", string.Empty);
            query = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(query);
            query = Uri.EscapeDataString(query);

            var parameters = new Dictionary<string, string>
            {
                { "key", this.key },
                { "q", query },
                { "searchtitle", string.Empty },
                { "page", page.ToString() }
            };

            var requestUri = BuildUri("https://www.goodreads.com/search.xml", parameters);
            var request = CreateRequest(requestUri);
            
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                if (stream == null || stream.CanRead == false)
                {
                    yield break;
                }

                var document = XDocument.Load(stream);
                var serializer = new XmlSerializer(typeof (GoodreadsWork));

                foreach (var work in document.Descendants("work"))
                {
                    using (var reader = work.CreateReader())
                    {
                        if (serializer.CanDeserialize(reader))
                        {
                            yield return serializer.Deserialize(reader) as GoodreadsWork;
                        }
                    }
                }
            }
        }
    }
}