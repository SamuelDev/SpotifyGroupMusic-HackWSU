using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpotifyGroupMusicHackWSU.Models;

namespace SpotifyGroupMusicHackWSU.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SpotifyAuthorization()
        {
            return Redirect(string.Format("https://accounts.spotify.com/authorize?client_id={0}&redirect_uri={1}&scope={2}&response_type=token&state=123", Properties.Resources.ClientId, Url.Action("Index", "Dashboard", null, Request.Url.Scheme, null), "playlist-read-private playlist-read-collaborative playlist-modify-public playlist-modify-private streaming user-follow-modify user-follow-read user-library-read user-library-modify user-read-private user-read-birthdate user-read-email user-top-read"));
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