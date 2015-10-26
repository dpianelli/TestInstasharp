using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using InstaSharp;
using InstaSharp.Models.Responses;

namespace TestInstasharp.Controllers
{
    public class HomeController : Controller
    {
        readonly InstagramConfig _config = new InstagramConfig(Common.Configuration.Instance.InstagramConfig.ClientId, Common.Configuration.Instance.InstagramConfig.ClientSecret, Common.Configuration.Instance.InstagramConfig.RedirectUri, "");


        // GET: Home
        public ActionResult Dashboard()
        {
            var oAuthResponse = Session["InstaSharp.AuthInfo"] as OAuthResponse;

            if (oAuthResponse == null)
            {
                return RedirectToAction("Login");
            }

            return View(oAuthResponse.User);
        }

        public ActionResult Login()
        {
            var scopes = new List<OAuth.Scope>
            {
                InstaSharp.OAuth.Scope.Likes,
                InstaSharp.OAuth.Scope.Comments,
                InstaSharp.OAuth.Scope.Relationships,
                InstaSharp.OAuth.Scope.Basic
            };

            var link = InstaSharp.OAuth.AuthLink(_config.OAuthUri + "authorize", _config.ClientId, _config.RedirectUri, scopes, InstaSharp.OAuth.ResponseType.Code);

            return Redirect(link);
        }

        public async Task<ActionResult> MyFeed()
        {
            var oAuthResponse = Session["InstaSharp.AuthInfo"] as OAuthResponse;

            if (oAuthResponse == null)
            {
                return RedirectToAction("Login");
            }

            var users = new InstaSharp.Endpoints.Users(_config, oAuthResponse);

            var feed = await users.Feed(null, null, null);

            return View(feed.Data);
        }

        public async Task<ActionResult> OAuth(string code)
        {
            // add this code to the auth object
            var auth = new OAuth(_config);

            // now we have to call back to instagram and include the code they gave us
            // along with our client secret
            var oauthResponse = await auth.RequestToken(code);

            // both the client secret and the token are considered sensitive data, so we won't be
            // sending them back to the browser. we'll only store them temporarily.  If a user's session times
            // out, they will have to click on the authenticate button again - sorry bout yer luck.
            Session.Add("InstaSharp.AuthInfo", oauthResponse);

            // all done, lets redirect to the home controller which will send some intial data to the app
            return RedirectToAction("Dashboard");
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Home Screen";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}