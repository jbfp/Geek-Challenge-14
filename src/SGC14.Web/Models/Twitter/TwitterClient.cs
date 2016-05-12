using SGC14.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC14.Web.Models.Twitter
{
    public class TwitterClient : HttpWebContentProvider, ITwitterClient
    {
        private readonly TwitterCredentials credentials;

        public TwitterClient(TwitterCredentials credentials)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException("credentials");
            }

            this.credentials = credentials;
        }

        public async Task<Maybe<string>> RequestJsonAsync(string uri, IDictionary<string, string> parameters)
        {
            var requestUri = BuildUri(uri, parameters);
            var request = ConstructHttpWebRequest(requestUri, "GET", parameters);

            try
            {
                using (var response = await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                {
                    if (stream == null || stream.CanRead == false)
                    {
                        return Maybe<string>.None;
                    }

                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            catch (Exception)
            {
                return Maybe<string>.None;
            }
        }

        public IObservable<string> OpenStreamAsync(string uri, IDictionary<string, string> parameters)
        {
            var requestUri = BuildUri(uri, parameters);
            var request = ConstructHttpWebRequest(requestUri, "POST", parameters);

            try
            {
                return Observable.StartAsync(ct => request.GetResponseAsync())
                                 .Select(response => response.GetResponseStream())
                                 .Where(stream => stream != null && stream.CanRead)
                                 .SelectMany(stream => Observable.Using(() => new StreamReader(stream, Encoding.UTF8),
                                                                        reader => Observable.Generate(reader, r =>
                                                                        {
                                                                            try
                                                                            {
                                                                                // Generate while EndOfStream is false.
                                                                                return !r.EndOfStream;
                                                                            }
                                                                            catch
                                                                            {
                                                                                // The EndOfStream property can sometimes produce IOExceptions for some odd reason,
                                                                                // so we put this ugly try-catch block here.
                                                                                return false;
                                                                            }
                                                                        }, r => r, r => r.ReadLine())))
                                 .Where(json => !string.IsNullOrEmpty(json));
            }
            catch
            {
                return Observable.Empty<string>();
            }
        }

        private HttpWebRequest ConstructHttpWebRequest(Uri requestUri, string method, IDictionary<string, string> parameters)
        {
            var authorizationParameter = OAuth.GetAuthorizationParameter(method, requestUri, this.credentials, parameters);
            var request = CreateRequest(requestUri);
            request.Headers.Add(HttpRequestHeader.Authorization, "OAuth " + authorizationParameter);
            request.Method = method;
            return request;
        }
    }
}