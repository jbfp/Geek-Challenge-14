using SGC14.Web.Models;
using SGC14.Web.Models.Twitter;
using System;
using System.Collections.Immutable;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGC14.Web.Controllers
{
    [Authorize]
    [RequireHttps]
    public class SearchController : Controller
    {
        private readonly ITwitterClientFactory twitterClientFactory;

        public SearchController(ITwitterClientFactory twitterClientFactory)
        {
            if (twitterClientFactory == null)
            {
                throw new ArgumentNullException("twitterClientFactory");
            }

            this.twitterClientFactory = twitterClientFactory;
        }

        // GET: /

        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /search

        [HttpGet]
        [Route("search")]
        public ActionResult Search(string query, int page = 1, string language = "")
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            // TODO: Save query.

            return View(new SearchQuery
            {
                Query = query,
                Page = page,
                Language = language
            });
        }

        // GET: /suggestions

        [HttpGet]
        [Route("suggestions")]
        public async Task<ActionResult> Suggestions()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
            {
                return new HttpUnauthorizedResult();
            }

            var credentials = TwitterCredentials.GetFromClaimsIdentity(claimsIdentity);
            var client = this.twitterClientFactory.Create(credentials);
            var twitter = new Twitter(client);
            IImmutableList<string> suggestions;

            try
            {
                suggestions = await twitter.GetSuggestionsAsync();
            }
            catch (WebException)
            {
                suggestions = ImmutableList<string>.Empty;
            }

            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }

        // GET: /about

        [HttpGet]
        [Route("about")]
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
    }
}