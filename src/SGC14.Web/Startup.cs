using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Twitter;
using Owin;
using SGC14.Web.Models.Twitter;
using System.Configuration;

[assembly: OwinStartup(typeof(SGC14.Web.Startup))]

namespace SGC14.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth/login"),
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"],
                Provider = new TwitterTokensAuthenticationProvider()
            });

            // Map SignalR must be below authentication, so SignalR can properly authorize.
            // Something, something pipeline order.
            app.MapSignalR();
        }
    }
}