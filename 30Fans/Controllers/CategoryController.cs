using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using Domain;
using System.IO;

namespace _30Fans.Controllers{
    public class CategoryController : BaseController{
        private ProductDao _productDao;
        private CategoryDao _categoryDao;
        private CategoryItemDao _categoryItemDao;

        public CategoryController() {
            _productDao = new ProductDao();
            _categoryDao = new CategoryDao();
            _categoryItemDao = new CategoryItemDao();
        }

        //
        // GET: /Football/
        public ActionResult ListAll() {
            var categorias = _categoryDao.GetaAll();
            return View(categorias);
        }

        //
        // GET: /Index/Football
        public ActionResult Index(string categoryName) {
            var categoria = _categoryDao.GetByName(categoryName);
            if (!categoria.Enable)
                return RedirectToAction("ComingSoon", "Home");

            return View(categoria);
        }
        
        // GET: /Category/Item/5
        public ActionResult Item(int id){
            IList<Product> products = null;
            try {
                var categoryItem = _categoryItemDao.Get(id);
                if (!categoryItem.Enable)
                    return RedirectToAction("ComingSoon", "Home");
                products = _productDao.GetByCategoryItemId(id);
                ViewBag.Title = products[0].CategoryItem.ItemName;
            }catch(ProductNotFoundException ex){
                ViewBag.Title = "Produto não encontrado";
                products = new List<Product>();
            }
            return View(products);
        }

        //
        // GET: /Admin/Edit/5 
        [Authorize]
        public ActionResult Edit(int id) {
            var category = _categoryDao.Get(id);
            return View(category);
        }

        //
        // POST: /Admin/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, Category category) {
            try {
                string aNewCategoryName = category.CategoryName;
                if (CategoryNameAlreadyExists(aNewCategoryName, id)) {
                    throw new Exception("Category name already exists!");
                }

                _categoryDao.Update(category);
                return RedirectToAction("Index" , "Admin");
            } catch {
                return View();
            }
        }

        [Authorize]
        public ActionResult UploadImage(long id) {
            var category = _categoryDao.Get(id);
            return View(category);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadImage(HttpPostedFileBase file, string id, string imageFolderName, FormCollection collection) {
            if (file != null && file.ContentLength > 0) {
                var category = _categoryDao.Get(Convert.ToInt64(id));

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), fileName);
                file.SaveAs(path);

                CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), imageFolderName));
                UpdateCategory(category, fileName, extension);
            } else {
                return View();
            }
            return RedirectToAction("Index","Admin");
        }

        [Authorize]
        public ActionResult UploadImageCategoryItem(long id) {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadImageCategoryItem(HttpPostedFileBase file, string id, string imageFolderName, FormCollection collection) {
            if (file != null && file.ContentLength > 0) {
                var categoryItem = _categoryItemDao.Get(Convert.ToInt64(id));

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(categoryItem.ImageUrl), fileName);
                file.SaveAs(path);

                CreateFolder(Path.Combine(Server.MapPath(categoryItem.ImageUrl), imageFolderName));
                UpdateCategoryItem(categoryItem, Path.GetFileNameWithoutExtension(file.FileName), extension);
            } else {
                return View();
            }
            return RedirectToAction("Index","Admin");
        }

        //
        // GET: /Admin/Edit/5 
        [Authorize]
        public ActionResult Delete(int id) {
            var category = _categoryDao.Get(id);
            return View(category);
        }

        //
        // POST: /Admin/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                var category = _categoryDao.Get(id);
                _categoryDao.Delete(category);
                return RedirectToAction("Index", "Admin");
            } catch {
                return View();
            }
        }

        [Authorize]
        public ActionResult AddCategoryItem(long id) {
            ViewBag.CategoryId = id;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCategoryItem(CategoryItem categoryItem, long CategoryId) {
            try {
                categoryItem.Category = _categoryDao.Get(CategoryId);
                _categoryItemDao.Save(categoryItem);
                return RedirectToAction("Edit", new { id = CategoryId });
            } catch {
                return View();
            }
        }

        [Authorize]
        public ActionResult EditCategoryItem(long id) {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCategoryItem(long id, CategoryItem categoryItem) {
            try {
                _categoryItemDao.Update(categoryItem);
                return RedirectToAction("Edit", new { id = categoryItem.Category.Id });
            } catch (Exception ex) {
                var teste = ex;
                return View();
            }
        }

        [Authorize]
        public ActionResult DeleteCategoryItem(long id) {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteCategoryItem(long id, CategoryItem categoryItem) {
            try {
                _categoryItemDao.Delete(categoryItem);
                var refreshedCategoryItem = _categoryItemDao.Get(id);
                return RedirectToAction("Edit", new { id = refreshedCategoryItem.Category.Id });
            } catch {
                return View();
            }
        }

        private bool CategoryNameAlreadyExists(string categoryName, long idNewCategory) {
            Category category = _categoryDao.GetByName(categoryName);
            return category != null && category.Id != idNewCategory;
        }

        private void CreateFolder(string folderPath) {
            if (!Directory.Exists(folderPath)){
                Directory.CreateDirectory(folderPath);
            }
        }

        private void UpdateCategory(Category category, string fileName, string extension) {
            fileName = fileName.Split('.')[0];
            category.ImageName = fileName;
            category.ImageExtension = extension;
            _categoryDao.Update(category);
        }

        private void UpdateCategoryItem(CategoryItem categoryItem, string fileName, string extension) {
            categoryItem.ImageName = fileName;
            categoryItem.ImageExtension = extension;
            _categoryItemDao.Update(categoryItem);
        }

    } //class
}
