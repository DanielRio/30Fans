using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using System.Web.UI;
using Dao.Impl;
using _30Fans.Misc;
using System.IO;

namespace _30Fans.Controllers{
    public class CategoryItemController : BaseController{
        private CategoryItemDao _categoryItemDao;
        private ProductDao _productDao;
        private CategoryDao _categoryDao;
        private FileSystemService _fileSystemService;

        public CategoryItemController(){
            _categoryItemDao = new CategoryItemDao();
            _productDao = new ProductDao();
            _categoryDao = new CategoryDao();
            _fileSystemService = new FileSystemService();
        }

        // GET: /Category/Item/5
        [OutputCache(Duration = 180, VaryByParam = "id", Location = OutputCacheLocation.Server)]
        public ActionResult Show(int id) {
            IList<Product> products = null;
            try {
                var categoryItem = _categoryItemDao.Get(id);
                if (!categoryItem.Enable)
                    return RedirectToAction("ComingSoon", "Home");
                products = _productDao.GetByCategoryItemId(id);
                ViewBag.Title = products[0].CategoryItem.ItemName;
            } catch (ProductNotFoundException ex) {
                ViewBag.Title = "Produto não encontrado";
                products = new List<Product>();
            }
            return View(products);
        }

        [Authorize]
        public ActionResult Create(long id) {
            ViewBag.CategoryId = id;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CategoryItem categoryItem, long CategoryId) {
            try {
                categoryItem.Category = _categoryDao.Get(CategoryId);
                _categoryItemDao.Save(categoryItem);
                return RedirectToAction("Edit", "Category", new { id = CategoryId });
            } catch {
                return View();
            }
        }

        [Authorize]
        public ActionResult Edit(long id) {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(long id, CategoryItem categoryItem) {
            try {
                _categoryItemDao.Update(categoryItem);
                return RedirectToAction("Edit","Category", new { id = categoryItem.Category.Id });
            } catch (Exception ex) {
                var teste = ex;
                return View();
            }
        }

        [Authorize]
        public ActionResult UploadImage(long id) {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadImage(HttpPostedFileBase file, string id, FormCollection collection) {
            CategoryItem categoryItem = null;
            if (file != null && file.ContentLength > 0) {
                categoryItem = _categoryItemDao.Get(Convert.ToInt64(id));

                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), categoryItem.Category.CategoryName));
                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(categoryItem.GetImagePath()), categoryItem.ItemName));

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(categoryItem.GetImagePath()), fileName);
                file.SaveAs(path);

                UpdateCategoryItem(categoryItem, Path.GetFileNameWithoutExtension(file.FileName), extension);
            } else {
                return View();
            }
            return RedirectToAction("Edit", "Category", new { id = categoryItem.Category.Id});
        }

        [Authorize]
        public ActionResult Delete(long id) {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(long id, CategoryItem categoryItem) {
            try {
                var refreshedCategoryItem = _categoryItemDao.Get(id);
                var categoryId = refreshedCategoryItem.Category.Id;
                _categoryItemDao.Delete(categoryItem);
                
                return RedirectToAction("Edit", new { id = categoryId });
            } catch {
                return View();
            }
        }

        private void UpdateCategoryItem(CategoryItem categoryItem, string fileName, string extension) {
            categoryItem.ImageName = fileName;
            categoryItem.ImageExtension = extension;
            _categoryItemDao.Update(categoryItem);
        }
    }// class
} //namespace
 