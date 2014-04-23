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
                    identifier = "Mb8bAF2lZsG";
                    break;
                case "hits":
                    identifier = "Gjj5AVRh9jR";
                    break;
                case "cartoons":
                    identifier = "kl4bIz0gpzm";
                    break;
                case "magic":
                    identifier = "qPtgSB73bEA";
                    break;
                case "football":
                    identifier = "QEf4eG8F6Iz";
                    break;
                case "basketball":
                    identifier = "Y4_y8Vs7do2";
                    break;
                case "cricket":
                    identifier = "GiB9Jj8tkwo";
                    break;
                case "wtf":
                    identifier = "KXIGNzNwaO4";
                    break;
                default:
                    throw new Exception("category doesn't found");
            }
            return identifier;
        }
    } // class
}
