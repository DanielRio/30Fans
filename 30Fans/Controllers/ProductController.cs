using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using System.Web.UI;
using Domain;

namespace _30Fans.Controllers{
    public class ProductController : BaseController{
        private ProductDao _productDao;
        private CategoryItemDao _categoryItemDao;

        public ProductController() {
            _productDao = new ProductDao();
            _categoryItemDao = new CategoryItemDao();
        }

        //
        // GET: /Product/SlideShow/5
        [OutputCache(Duration = 180, VaryByParam="id" , Location = OutputCacheLocation.Server)]
        public ActionResult SlideShow(int id){
            var product = _productDao.Get(id);
            if (!product.Enable)
                return RedirectToAction("ComingSoon", "Home");

            return View(product);
        }

        //
        // GET: /Product/SlideShow/5
        public ActionResult List(int id) {
            IList<Product> products = null;
            ViewBag.CategoryItemId = id;
            try {
                products = _productDao.GetByCategoryItemId(id);
            } catch (ProductNotFoundException) {
                products = new List<Product>();
            }
            return PartialView("_AdminProductList", products);
        }

        [Authorize]
        public ActionResult Create(long idCategoryItem) {
            var categoryItem = _categoryItemDao.Get(idCategoryItem);
            ViewBag.CategoryItemId = categoryItem.Id;
            ViewBag.CategoryItemName = categoryItem.ItemName;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Product product, long CategoryItemId) {
            try {
                product.CategoryItem = _categoryItemDao.Get(CategoryItemId);
                product.AvailableQuantity = 30;
                product.PublishedDate = DateTime.Now;
                _productDao.Save(product);
                return RedirectToAction("Edit", "CategoryItem", new { id = product.CategoryItem.Id });
            } catch {
                return View();
            }
        }

        [Authorize]
        public ActionResult Edit(long id) {
            var product = _productDao.Get(id);
            return View(product);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(long id, Product product) {
            try {
                var originalProduct = _productDao.Get(id);
                product.PublishedDate = originalProduct.PublishedDate;
                product.CategoryItem = originalProduct.CategoryItem;
                _productDao.Update(product);
                return RedirectToAction("Edit", "CategoryItem", new { id = product.CategoryItem.Id });
            } catch (Exception) {
                return View();
            }
        }
    } // class
}
