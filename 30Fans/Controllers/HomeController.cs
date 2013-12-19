using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using _30Fans.Misc;


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

        public ActionResult Contact() {
            return View();
        }

        public ActionResult Conditions() {
            return View();
        }

        public ActionResult ThankYou() {
            return View();
        }

        public ActionResult Videos() {
            return View();
        }

        public ActionResult ComingSoon() {
            return View();
        }

        public ActionResult ChangeLanguage(string id) {
            ILanguageHandler languageHandler = new LanguageHandler();
            languageHandler.SetLanguage(id);
            return RedirectToAction("Index");
        }
        
    } // class
}
