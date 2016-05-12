using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SGC14.Web.Models.Twitter
{
    internal class OAuth
    {
        private static readonly Random Random = new Random();

        public static string GetAuthorizationParameter(string method, Uri uri, TwitterCredentials credentials, IDictionary<string, string> parameters)
        {
            parameters["oauth_consumer_key"] = credentials.ConsumerKey;
            parameters["oauth_nonce"] = GenerateNonce();
            parameters["oauth_signature_method"] = "HMAC-SHA1";
            parameters["oauth_timestamp"] = GetTimestamp();
            parameters["oauth_token"] = credentials.AccessToken;      
            parameters["oauth_version"] = "1.0";      

            string encodedAndSortedString = BuildEncodedSortedString(parameters);
            string signatureBaseString = BuildSignatureBaseString(method.ToString(), uri.ToString(), encodedAndSortedString);
            string signingKey = BuildSigningKey(credentials.ConsumerSecret, credentials.AccessTokenSecret);
            string signature = CalculateSignature(signingKey, signatureBaseString);
            return BuildAuthorizationHeaderString(encodedAndSortedString, signature);
        }

        private static string BuildEncodedSortedString(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            return string.Join("&",
                (from parameter in parameters
                 let key = parameter.Key
                 let value = Uri.EscapeDataString(parameter.Value)
                 orderby parameter.Key
                 select string.Format("{0}={1}", key, value)));
        }

        private static string BuildSignatureBaseString(string method, string url, string encodedStringParameters)
        {
            int paramsIndex = url.IndexOf('?');
            string urlWithoutParams = paramsIndex >= 0 ? url.Substring(0, paramsIndex) : url;
            return string.Join("&", new[]
            {
                method.ToUpper(),
                Uri.EscapeDataString(urlWithoutParams),
                Uri.EscapeDataString(encodedStringParameters)
            });
        }

        private static string BuildSigningKey(string consumerSecret, string accessTokenSecret)
        {
            return string.Format(
                CultureInfo.InvariantCulture, "{0}&{1}",
                Uri.EscapeDataString(consumerSecret),
                Uri.EscapeDataString(accessTokenSecret));
        }

        private static string CalculateSignature(string signingKey, string signatureBaseString)
        {
            byte[] key = Encoding.UTF8.GetBytes(signingKey);
            byte[] msg = Encoding.UTF8.GetBytes(signatureBaseString);
            
            using (var hmacsha1 = new HMACSHA1(key))
            {
                var hash = hmacsha1.ComputeHash(msg);
                return Convert.ToBase64String(hash);                
            }
        }

        private static string BuildAuthorizationHeaderString(string encodedAndSortedString, string signature)
        {
            string[] allParms = (encodedAndSortedString + "&oauth_signature=" + Uri.EscapeDataString(signature)).Split('&');
            string allParmsString =
                string.Join(", ",
                    (from parm in allParms
                     let keyVal = parm.Split('=')
                     where parm.StartsWith("oauth") || parm.StartsWith("x_auth")
                     orderby keyVal[0]
                     select keyVal[0] + "=\"" + keyVal[1] + "\""));
            return allParmsString;
        }

        private static string GetTimestamp()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var timeSpan = DateTime.UtcNow - epoch;
            var totalSeconds = timeSpan.TotalSeconds;
            return Math.Floor(totalSeconds).ToString(CultureInfo.InvariantCulture);
        }

        private static string GenerateNonce()
        {
            return Random.Next(111111, 9999999).ToString(CultureInfo.InvariantCulture);
        }
    }
}