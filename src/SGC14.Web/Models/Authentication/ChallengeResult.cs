using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace SGC14.Web.Models.Authentication
{
    class ChallengeResult : HttpUnauthorizedResult
    {
        private const string XsrfKey = "XsrfId";

        private readonly string loginProvider;
        private readonly string redirectUri;
        private readonly string userId;

        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string loginProvider, string redirectUri, string userId)
        {
            this.loginProvider = loginProvider;
            this.redirectUri = redirectUri;
            this.userId = userId;
        }

        public string LoginProvider
        {
            get { return this.loginProvider; }
        }

        public string RedirectUri
        {
            get { return this.redirectUri; }
        }

        public string UserId
        {
            get { return this.userId; }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = this.RedirectUri };

            if (this.UserId != null)
            {
                properties.Dictionary[XsrfKey] = this.UserId;
            }

            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, this.LoginProvider);
        }
    }
}