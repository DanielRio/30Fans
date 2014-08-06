using _30Fans.Web.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _30Fans.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ChangeLanguage(string id)
        {
            ILanguageHandler languageHandler = new LanguageHandler();
            languageHandler.SetLanguage(id);
            return RedirectToAction("Index");
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}