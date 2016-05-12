using Microsoft.Owin.Security.Twitter;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SGC14.Web.Models.Twitter
{
    internal class TwitterTokensAuthenticationProvider : TwitterAuthenticationProvider
    {
        public override Task Authenticated(TwitterAuthenticatedContext context)
        {
            context.Identity.AddClaims(new[]
            {
                new Claim(TwitterClaimTypes.AccessToken, context.AccessToken),
                new Claim(TwitterClaimTypes.AccessTokenSecret, context.AccessTokenSecret)
            });

            return base.Authenticated(context);
        }
    }
}