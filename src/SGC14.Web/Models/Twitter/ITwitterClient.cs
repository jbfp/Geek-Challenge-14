using SGC14.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGC14.Web.Models.Twitter
{
    public interface ITwitterClient
    {
        Task<Maybe<string>> RequestJsonAsync(string uri, IDictionary<string, string> parameters);
        IObservable<string> OpenStreamAsync(string uri, IDictionary<string, string> parameters);
    }
}