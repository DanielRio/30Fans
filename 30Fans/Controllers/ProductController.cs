using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;

namespace _30Fans.Controllers{
    public class ProductController : BaseController{
        private ProductDao _productDao;

        public ProductController() {
            _productDao = new ProductDao();
        }

        //
        // GET: /Product/SlideShow/5
        public ActionResult SlideShow(int id){
            var product = _productDao.Get(id);
            if (!product.Enable)
                return RedirectToAction("ComingSoon", "Home");

            return View(product);
        }
    } // class
}
