using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SGC14.Web.Models
{
    public class HttpWebContentProvider
    {
        protected const string UserAgent = "SGC14 1.0";

        protected static HttpWebRequest CreateRequest(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }

            var request = WebRequest.CreateHttp(requestUri);
            request.UserAgent = UserAgent;
            return request;
        }

        protected static Uri BuildUri(string uri, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            return new UriBuilder(uri)
            {
                Query = string.Join("&", from parameter in parameters
                                         let key = Uri.EscapeDataString(parameter.Key)
                                         let value = Uri.EscapeDataString(parameter.Value)
                                         select string.Format("{0}={1}", key, value))
            }.Uri;
        }
    }
}