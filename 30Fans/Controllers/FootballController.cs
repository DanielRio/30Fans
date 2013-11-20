using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using System.Web.UI;

namespace _30Fans.Controllers{
    public class FootballController : BaseController{
        private CategoryDao _categoryDao;
        private const string FOOTBALL_CATEGORY = "Football";

        public FootballController() {
            _categoryDao = new CategoryDao();
        }

        //
        // GET: /Football/
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        public ActionResult Index(){
            var categoria = _categoryDao.GetByName(FOOTBALL_CATEGORY);
            if (!categoria.Enable)
                return RedirectToAction("ComingSoon", "Home");

            return View(categoria);
        }
    
    } //class
}
