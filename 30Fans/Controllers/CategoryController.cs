using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using Domain;
using System.IO;
using System.Web.UI;
using _30Fans.Misc;

namespace _30Fans.Controllers{
    public class CategoryController : BaseController{
        private ProductDao _productDao;
        private CategoryDao _categoryDao;
        private CategoryItemDao _categoryItemDao;
        private FileSystemService _fileSystemService;

        public CategoryController() {
            _productDao = new ProductDao();
            _categoryDao = new CategoryDao();
            _categoryItemDao = new CategoryItemDao();
            _fileSystemService = new FileSystemService();
        }

        //
        // GET: /Category/ListAll/
        [OutputCache(Duration=180,Location=OutputCacheLocation.Server)]
        public ActionResult ListAll() {
            var categorias = _categoryDao.GetaAll();
            categorias = Priorize(categorias);
            return View(categorias);
        }

        private IList<Category> Priorize(IList<Category> categorias) {
            return categorias.OrderByDescending(x => x.Priority).ToList();
        }

        //
        // GET: /Index/Football
        [OutputCache(Duration = 180, VaryByParam = "categoryName", Location = OutputCacheLocation.Server)]
        public ActionResult Index(string id) {
            var categoryName = id;
            var categoria = _categoryDao.GetByName(id);
            if (!categoria.Enable)
                return RedirectToAction("ComingSoon", "Home");

            return View(categoria);
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

                category.Items = _categoryItemDao.GetByCategoryId(id);
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
        public ActionResult UploadImage(HttpPostedFileBase file, string id,FormCollection collection) {
            if (file != null && file.ContentLength > 0) {
                var category = _categoryDao.Get(Convert.ToInt64(id));

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), fileName);
                file.SaveAs(path);

                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), category.CategoryName));
                UpdateCategory(category, fileName, extension);
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

        private bool CategoryNameAlreadyExists(string categoryName, long idNewCategory) {
            Category category = _categoryDao.GetByName(categoryName);
            return category != null && category.Id != idNewCategory;
        }

        private void UpdateCategory(Category category, string fileName, string extension) {
            fileName = fileName.Split('.')[0];
            category.ImageName = fileName;
            category.ImageExtension = extension;
            _categoryDao.Update(category);
        }

    } //class
}
