using Dao.Impl;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace _30Fans.Web.Controllers
{
    public class FootballController : Controller
    {
        private CategoryItemDao _categoryItemDao;
        private CategoryDao _categoryDao;
        private const string FOOTBALL_CATEGORY = "Football";
        private ProductDao _productDao;

        public FootballController()
        {
            _categoryItemDao = new CategoryItemDao();
            _productDao = new ProductDao();
            _categoryDao = new CategoryDao();
        }

        //
        // GET: /Football/
        [OutputCache(Duration = 60, Location = OutputCacheLocation.Server)]
        public ActionResult Index_old()
        {
            var categoria = _categoryDao.GetByName(FOOTBALL_CATEGORY);
            if (!categoria.Enable)
                return RedirectToAction("ComingSoon", "Home");

            return View(categoria);
        }




        [OutputCache(Duration = 180, VaryByParam = "id", Location = OutputCacheLocation.Server)]
        public ActionResult Index(int id)
        {
            List<string> fanImages = new List<string>();
            try
            {
                fanImages = System.IO.Directory.GetFiles(Server.MapPath("~/Images/Fans/2014/" + id)).ToList();
                for (int i = 0; i < fanImages.Count; i++)
                {
                    fanImages[i] = Path.GetFileName(fanImages[i]);
                }
            }
            catch
            {
            }
            ViewBag.FanImages = fanImages;
            var product = _productDao.Get(id);
            if (!product.Enable)
                return RedirectToAction("ComingSoon", "Home");

            return View(product);

        }


        public PartialViewResult SelectedFriendFilter(string partName)
        {
            IList<Product> lista = _productDao.GetByPartName(partName);
            return PartialView("_TeamSearch", lista);
        }
    }
}