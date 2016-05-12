using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SGC14.Web.Models.Authentication;
using SGC14.Web.Models.Twitter;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using User = SGC14.Web.Models.Authentication.User;

namespace SGC14.Web.Controllers
{
    [Authorize]
    [RequireHttps]
    [RoutePrefix("auth")]
    public class AuthenticationController : Controller
    {
        private readonly Func<UserManager<User, Guid>> userManagerFactory;

        public AuthenticationController(Func<UserManager<User, Guid>> userManagerFactory)
        {
            if (userManagerFactory == null)
            {
                throw new ArgumentNullException("userManagerFactory");
            }

            this.userManagerFactory = userManagerFactory;            
        }

        // GET: /auth/login

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /auth/login

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Authentication", new { ReturnUrl = returnUrl }));
        }

        // GET: /auth/external

        [HttpGet]
        [Route("external")]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            IAuthenticationManager authenticationManager = GetAuthenticationManager();
            ExternalLoginInfo loginInfo = await authenticationManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                return RedirectToAction("ExternalLogin", new { ReturnUrl = returnUrl });
            }

            using (var userManager = this.userManagerFactory())
            {
                // Register Twitter user, if does he/she does not exist in our database.
                var user = userManager.FindByName(loginInfo.DefaultUserName);

                if (user == null)
                {
                    user = new User(loginInfo.DefaultUserName);
                    await userManager.CreateAsync(user);
                    await userManager.AddLoginAsync(user.Id, loginInfo.Login);
                }

                // Add/Update access tokens in the database, so we can search on behalf of the user.
                var accessToken = loginInfo.ExternalIdentity.FindFirst(TwitterClaimTypes.AccessToken);
                var accessTokenSecret = loginInfo.ExternalIdentity.FindFirst(TwitterClaimTypes.AccessTokenSecret);
                var existingClaims = await userManager.GetClaimsAsync(user.Id);

                if (existingClaims.Any(c => c.Type == accessToken.Type))
                {
                    await userManager.RemoveClaimAsync(user.Id, accessToken);
                }

                await userManager.AddClaimAsync(user.Id, accessToken);

                if (existingClaims.Any(c => c.Type == accessTokenSecret.Type))
                {
                    await userManager.RemoveClaimAsync(user.Id, accessTokenSecret);
                }

                await userManager.AddClaimAsync(user.Id, accessTokenSecret);

                // Sign user in with cookies.
                var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                authenticationManager.SignIn(identity);
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Search", "Search");
        }

        // POST: /auth/logout

        [HttpPost]
        [Route("logout")]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            GetAuthenticationManager().SignOut();
            return RedirectToAction("ExternalLogin", "Authentication");
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            return HttpContext.GetOwinContext().Authentication;
        }
    }
}