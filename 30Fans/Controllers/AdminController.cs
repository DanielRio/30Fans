using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using _30Fans.Models;
using Domain;
using _30Fans.Misc;
using System.IO;

namespace _30Fans.Controllers{
    [Authorize()]
    public class AdminController : BaseController{
        private CategoryDao _categoryDao;
        private CategoryItemDao _categoryItemDao;
        private ProductDao _productDao;
        private FileSystemService _fileSystemService;
        public AdminController() {
            _categoryDao = new CategoryDao();
            _categoryItemDao = new CategoryItemDao();
            _productDao = new ProductDao();
            _fileSystemService = new FileSystemService();
        }
        //
        // GET: /Admin/
        public ActionResult Index(){
            var adminViewModel = new AdminModel();
            adminViewModel.Categoies = _categoryDao.GetaAll();
            return View(adminViewModel);
        }

        //
        // GET: /Admin/Create
        public ActionResult CreateCategory() {
            return View();
        } 

        //
        // POST: /Admin/Create
        [HttpPost]
        public ActionResult CreateCategory(Category category) {
            try{
                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), category.CategoryName));
                _categoryDao.Save(category);
                return RedirectToAction("Index");
            }
            catch{
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5 
        public ActionResult AddPhoto(){
            var categories = _categoryDao.GetaAll();
            categories.Insert(0, new Category() { Id = 0, CategoryName = "-- Select a Category --" });
            return View(categories);
        }

        public JsonResult CategoryItem(string id) {
            long categoryId = Convert.ToInt64(id);
            var categoriesItem = _categoryItemDao.GetByCategoryId(categoryId);
            var initialText = string.Empty;
            if (categoriesItem == null || categoriesItem.Count == 0) {
                initialText = "There are no items in that category!";
            } else {
                initialText = string.Format("Loaded! Choose one item in {0}!", categoriesItem.FirstOrDefault().Category.CategoryName);
            }

            categoriesItem.Insert(0, new CategoryItem() { Id = 0, ItemName = initialText });
            var returnedJson = categoriesItem.Select(x => new { Id = x.Id, Value = x.ItemName }).ToList();
            return Json(returnedJson, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Products(string id) {
            long categoryItemId = Convert.ToInt64(id);
            var products = _productDao.GetByCategoryItemId(categoryItemId);
            var initialText = string.Empty;
            if (products == null || products.Count == 0) {
                initialText = "There are no products in that category!";
            } else {
                initialText = string.Format("Loaded! Choose one product in {0}!", products.FirstOrDefault().CategoryItem.ItemName);
            }

            products.Insert(0, new Product() { Id = 0, ProductName = initialText });
            var returnedJson = products.Select(x => new { Id = x.Id, Value = x.ProductName }).ToList();
            return Json(returnedJson, JsonRequestBehavior.AllowGet);
        }

    }// class
}
