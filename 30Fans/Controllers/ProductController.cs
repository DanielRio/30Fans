using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using System.Web.UI;
using Domain;
using _30Fans.Misc;
using System.IO;

namespace _30Fans.Controllers{
    public class ProductController : BaseController{
        private ProductDao _productDao;
        private CategoryItemDao _categoryItemDao;
        private FileSystemService _fileSystemService;

        public ProductController() {
            _productDao = new ProductDao();
            _categoryItemDao = new CategoryItemDao();
            _fileSystemService = new FileSystemService();
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
        [Authorize]
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

        [Authorize]
        public ActionResult UploadImage(long id) {
            var product = _productDao.Get(id);
            return View(product);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadImage(HttpPostedFileBase file, string id, FormCollection collection) {
            Product product = null;
            if (file != null && file.ContentLength > 0) {
                product = _productDao.Get(Convert.ToInt64(id));

                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), product.CategoryItem.Category.CategoryName));
                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(product.CategoryItem.GetImagePath()), product.CategoryItem.ItemName));
                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(product.GetImagePath()), product.ProductName));

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(product.GetImagePath()), fileName);
                file.SaveAs(path);

                UpdateProductItem(product, Path.GetFileNameWithoutExtension(file.FileName), extension);
            } else {
                return View();
            }
            return RedirectToAction("Edit", "CategoryItem", new { id = product.CategoryItem.Id });
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadPhoto(HttpPostedFileBase uploadFile, FormCollection collection) {
            if (string.IsNullOrEmpty(collection["ddProducts"]))
                throw new InvalidOperationException("Product Id does not found");
            long productId = Convert.ToInt64(collection["ddProducts"]);

            Product product = null;
            if (uploadFile != null && uploadFile.ContentLength > 0) {
                product = _productDao.Get(productId);
                var imageText = collection["imageText"];

                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), product.CategoryItem.Category.CategoryName));
                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(product.CategoryItem.GetImagePath()), product.CategoryItem.ItemName));
                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(product.GetImagePath()), product.ProductName));

                var fileName = Path.GetFileName(uploadFile.FileName);
                var extension = Path.GetExtension(uploadFile.FileName);

                product.AddPhoto(imageText, fileName, extension);
                _productDao.Update(product);

                var physicalFileName = product.GetLastSavedPhoto().Id.ToString() + extension;
                var path = Path.Combine(Server.MapPath(product.GetPhotoImagePath()), physicalFileName);
                uploadFile.SaveAs(path);
            } else {
                return View();
            }
            return RedirectToAction("AddPhoto", "Admin");
        }

        private void UpdateProductItem(Product product, string fileName, string extension) {
            product.ImageName = fileName;
            product.ImageExtension = extension;
            _productDao.Update(product);
        }
    } // class
}
