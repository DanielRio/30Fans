using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _30Fans.Controllers{
    public class ErrorController : BaseController{
        public ActionResult HttpError404(string error) {
            ViewBag.Title = "Sorry, an error occurred while processing your request. (404)";
            ViewBag.Description = error;
            //ViewBag.CompleteError = Session["tempError"];
            return View();
        }

        public ActionResult HttpError500(string error) {
            ViewBag.Title = "Sorry, an error occurred while processing your request. (500)";
            ViewBag.Description = error;
            //ViewBag.CompleteError = Session["tempError"];
            return View();
        }


        public ActionResult General(string error) {
            ViewBag.Title = "Sorry, an error occurred while processing your request.";
            ViewBag.Description = error;
            //ViewBag.CompleteError = Session["tempError"];
            return View();
        } 

    }
}
