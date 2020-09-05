using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace plground.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _pass = ConfigurationManager.AppSettings["pass"];

        [Authorize]
        public ActionResult Index()
        {
            var filenames = Directory.EnumerateFiles(Server.MapPath("~/Videos"))
                .Select(x => Path.GetFileName(x))
                .OrderBy(x => x)
                .ToList();

            return View(filenames);
        }

        public ActionResult Authorize()
        {
            FormsAuthentication.SignOut();

            return View();
        }

        [HttpPost]
        public ActionResult Authorize(string password)
        {
            if (_pass == password)
                FormsAuthentication.RedirectFromLoginPage(_pass, true);

            return View();
        }
    }
}