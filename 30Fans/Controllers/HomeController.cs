using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using _30Fans.Misc;
using Domain;


namespace _30Fans.Controllers {
    public class HomeController : BaseController {
        private ProductDao _productDao;

        public HomeController() {
            _productDao = new ProductDao();
        }

        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View();
        }

        public ActionResult ThankYou() {
            return View();
        }

        public ActionResult Videos() {
            return View();
        }

        public ActionResult ShowVideo(string id) {
            var videoCategory = id;
            string plattaform = "//express.tubepress.com/v";
            string url = string.Empty;
            string identifier = GetVideoIdentifier(videoCategory);

            VideoGroup videoGroup = new VideoGroup(plattaform, videoCategory, identifier);
            return View(videoGroup);
        }

        public ActionResult ComingSoon() {
            return View();
        }

        public ActionResult ChangeLanguage(string id) {
            ILanguageHandler languageHandler = new LanguageHandler();
            languageHandler.SetLanguage(id);
            return RedirectToAction("Index");
        }


        private string GetVideoIdentifier(string videoCategory) {
            string identifier = string.Empty;
            switch (videoCategory.ToLower()) {
                case "accidents":
                    identifier = "62EG2dCFpjm";
                    break;
                case "hits":
                    identifier = "ZU8FjzHxM6u";
                    break;
                case "cartoons":
                    identifier = "sssIaInn1eE";
                    break;
                case "magic":
                    identifier = "lb0QuMSwf9W";
                    break;
                case "football":
                    identifier = "nbrEy0Tefrr";
                    break;
                case "basketball":
                    identifier = "ZKE4e8q9OSZ";
                    break;
                case "cricket":
                    identifier = "clR7CbG2lhq";
                    break;
                case "wtf":
                    identifier = "ocQ1Cvl7AZx";
                    break;
                default:
                    throw new Exception("category doesn't found");
            }
            return identifier;
        }
    } // class
}
