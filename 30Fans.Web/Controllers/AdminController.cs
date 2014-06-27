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
    public class ImagePathConstants
    {
        public const string CATEGORIES = "~/Images/Teams/Football/";


    }

    public class AdminController : Controller
    {
        private CategoryDao _categoryDao;
        private CategoryItemDao _categoryItemDao;
        private ProductDao _productDao;
        public AdminController() {
            _categoryDao = new CategoryDao();
            _categoryItemDao = new CategoryItemDao();
            _productDao = new ProductDao();
        }


        public void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        //
        // GET: /Admin/
        public ActionResult Index(){
            return View(_categoryDao.GetaAll());
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
                this.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), category.CategoryName));
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


        #region Produtos

        //
        // GET: /Product/SlideShow/5
        [OutputCache(Duration = 180, VaryByParam = "id", Location = OutputCacheLocation.Server)]
        public ActionResult SlideShow(int id)
        {
            List<string> fanImages = new List<string>();
            try
            {
                fanImages = System.IO.Directory.GetFiles(Server.MapPath("~/FanImages/2014/" + id)).ToList();
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

        //
        // GET: /Product/SlideShow/5
        //[Authorize]
        public ActionResult ListProduto(int id)
        {
            IList<Product> products = null;
            ViewBag.CategoryItemId = id;
            try
            {
                products = _productDao.GetByCategoryItemId(id);
            }
            catch (ProductNotFoundException)
            {
                products = new List<Product>();
            }
            return PartialView("_AdminProductList", products);
        }

       // [Authorize]
        public ActionResult CreateProduto(long idCategoryItem)
        {
            var categoryItem = _categoryItemDao.Get(idCategoryItem);
            ViewBag.CategoryItemId = categoryItem.Id;
            ViewBag.CategoryItemName = categoryItem.ItemName;
            return View();
        }

        //[Authorize]
        [HttpPost]
        public ActionResult CreateProduto(Product product, long CategoryItemId)
        {
            try
            {
                product.CategoryItem = _categoryItemDao.Get(CategoryItemId);
                product.AvailableQuantity = 30;
                product.PublishedDate = DateTime.Now;
                _productDao.Save(product);
                return RedirectToAction("Edit", "CategoryItem", new { id = product.CategoryItem.Id });
            }
            catch
            {
                return View();
            }
        }

       // [Authorize]
        public ActionResult EditProduto(long id)
        {
            var product = _productDao.Get(id);
            ViewBag.Produtos = _productDao.GetByCategoryItemId(id);
            ViewBag.CategoryItemId = id;
            return View(product);
        }

        //[Authorize]
        [HttpPost]
        public ActionResult EditProduto(long id, Product product)
        {
            try
            {
                var originalProduct = _productDao.Get(id);
                product.PublishedDate = originalProduct.PublishedDate;
                product.CategoryItem = originalProduct.CategoryItem;
                _productDao.Update(product);
                return RedirectToAction("Edit", "CategoryItem", new { id = product.CategoryItem.Id });
            }
            catch (Exception)
            {
                return View();
            }
        }

        //[Authorize]
        public ActionResult DeleteProduto(long id)
        {
            try
            {
                var originalProduct = _productDao.Get(id);
                long returnId = originalProduct.CategoryItem.Id;
                _productDao.Delete(originalProduct);

                return RedirectToAction("Edit", "CategoryItem", new { id = returnId });
            }
            catch (Exception)
            {
                return View();
            }
        }

        //[Authorize]
        public ActionResult UploadImageProduto(long id)
        {
            var product = _productDao.Get(id);
            return View(product);
        }

        [HttpPost]
        //[Authorize]
        public ActionResult UploadImageProduto(HttpPostedFileBase file, string id, FormCollection collection)
        {
            Product product = null;
            if (file != null && file.ContentLength > 0)
            {
                product = _productDao.Get(Convert.ToInt64(id));

                this.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), product.CategoryItem.Category.CategoryName));
                this.CreateFolder(Path.Combine(Server.MapPath(product.CategoryItem.GetImagePath()), product.CategoryItem.ItemName));
                this.CreateFolder(Path.Combine(Server.MapPath(product.GetImagePath()), product.ProductName));

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(product.GetImagePath()), fileName);
                file.SaveAs(path);

                UpdateProductItemProduto(product, Path.GetFileNameWithoutExtension(file.FileName), extension);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Edit", "CategoryItem", new { id = product.CategoryItem.Id });
        }

        [HttpPost]
        //[Authorize]
        public ActionResult UploadPhotoProduto(HttpPostedFileBase uploadFile, FormCollection collection)
        {
            if (string.IsNullOrEmpty(collection["ddProducts"]))
                throw new InvalidOperationException("Product Id does not found");
            long productId = Convert.ToInt64(collection["ddProducts"]);

            Product product = null;
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                product = _productDao.Get(productId);
                var imageText = collection["imageText"];

                this.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), product.CategoryItem.Category.CategoryName));
                this.CreateFolder(Path.Combine(Server.MapPath(product.CategoryItem.GetImagePath()), product.CategoryItem.ItemName));
                this.CreateFolder(Path.Combine(Server.MapPath(product.GetImagePath()), product.ProductName));

                var fileName = Path.GetFileName(uploadFile.FileName);
                var extension = Path.GetExtension(uploadFile.FileName);

                product.AddPhoto(imageText, fileName, extension);
                _productDao.Update(product);

                var physicalFileName = product.GetLastSavedPhoto().Id.ToString() + extension;
                var path = Path.Combine(Server.MapPath(product.GetPhotoImagePath()), physicalFileName);
                uploadFile.SaveAs(path);
            }
            else
            {
                return View();
            }
            return RedirectToAction("AddPhoto", "Admin");
        }

        private void UpdateProductItemProduto(Product product, string fileName, string extension)
        {
            product.ImageName = fileName;
            product.ImageExtension = extension;
            _productDao.Update(product);
        }

        #endregion


        #region Categoria

        //
        // GET: /Category/ListAll/
        [OutputCache(Duration = 180, Location = OutputCacheLocation.Server)]
        public ActionResult ListAllCategoria()
        {
            var categorias = _categoryDao.GetaAll();
            categorias = PriorizeCategoria(categorias);
            return View(categorias);
        }

        private IList<Category> PriorizeCategoria(IList<Category> categorias)
        {
            return categorias.OrderByDescending(x => x.Priority).ToList();
        }

        //
        // GET: /Index/Football
        [OutputCache(Duration = 180, VaryByParam = "categoryName", Location = OutputCacheLocation.Server)]
        public ActionResult IndexCategoria(string id)
        {
            var categoryName = id;
            var categoria = _categoryDao.GetByName(id);
            if (!categoria.Enable)
                return RedirectToAction("ComingSoon", "Home");

            return View(categoria);
        }

        //
        // GET: /Admin/Edit/5 
       // [Authorize]
        public ActionResult EditCategoria(int id)
        {
            var category = _categoryDao.Get(id);
            return View(category);
        }

        //
        // POST: /Admin/Edit/5
        [HttpPost]
        //[Authorize]
        public ActionResult EditCategoria(int id, Category category)
        {
            try
            {
                string aNewCategoryName = category.CategoryName;
                if (CategoryNameAlreadyExistsCategoria(aNewCategoryName, id))
                {
                    throw new Exception("Category name already exists!");
                }

                category.Items = _categoryItemDao.GetByCategoryId(id);
                _categoryDao.Update(category);
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return View();
            }
        }

        //[Authorize]
        public ActionResult UploadImageCategoria(long id)
        {
            var category = _categoryDao.Get(id);
            return View(category);
        }

        [HttpPost]
        //[Authorize]
        public ActionResult UploadImageCategoria(HttpPostedFileBase file, string id, FormCollection collection)
        {
            if (file != null && file.ContentLength > 0)
            {
                var category = _categoryDao.Get(Convert.ToInt64(id));

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), fileName);
                file.SaveAs(path);

                this.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), category.CategoryName));
                UpdateCategoryCategoria(category, fileName, extension);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index", "Admin");
        }

        //
        // GET: /Admin/Edit/5 
       // [Authorize]
        public ActionResult DeleteCategoria(int id)
        {
            var category = _categoryDao.Get(id);
            return View(category);
        }

        //
        // POST: /Admin/Edit/5
        [HttpPost]
       // [Authorize]
        public ActionResult DeleteCategoria(int id, FormCollection collection)
        {
            try
            {
                var category = _categoryDao.Get(id);
                _categoryDao.Delete(category);
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return View();
            }
        }

        private bool CategoryNameAlreadyExistsCategoria(string categoryName, long idNewCategory)
        {
            Category category = _categoryDao.GetByName(categoryName);
            return category != null && category.Id != idNewCategory;
        }

        private void UpdateCategoryCategoria(Category category, string fileName, string extension)
        {
            fileName = fileName.Split('.')[0];
            category.ImageName = fileName;
            category.ImageExtension = extension;
            _categoryDao.Update(category);
        }

        #endregion


        #region ItemCategoria

        // GET: /Category/Item/5
        [OutputCache(Duration = 180, VaryByParam = "id", Location = OutputCacheLocation.Server)]
        public ActionResult ShowItem(int id)
        {
            IList<Product> products = null;
            try
            {
                var categoryItem = _categoryItemDao.Get(id);
                if (!categoryItem.Enable)
                    return RedirectToAction("ComingSoon", "Home");
                products = _productDao.GetByCategoryItemId(id);
                ViewBag.Title = products[0].CategoryItem.ItemName;
            }
            catch (ProductNotFoundException ex)
            {
                ViewBag.Title = "Produto não encontrado";
                products = new List<Product>();
            }
            return View(products);
        }

        //[Authorize]
        public ActionResult CreateItem(long id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

       // [Authorize]
        [HttpPost]
        public ActionResult CreateItem(CategoryItem categoryItem, long CategoryId)
        {
            try
            {
                categoryItem.Category = _categoryDao.Get(CategoryId);
                _categoryItemDao.Save(categoryItem);
                return RedirectToAction("Edit", "Category", new { id = CategoryId });
            }
            catch
            {
                return View();
            }
        }

       // [Authorize]
        public ActionResult EditItem(long id)
        {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

        //[Authorize]
        [HttpPost]
        public ActionResult EditItem(long id, CategoryItem categoryItem)
        {
            try
            {
                _categoryItemDao.Update(categoryItem);
                return RedirectToAction("Edit", "Category", new { id = categoryItem.Category.Id });
            }
            catch (Exception ex)
            {
                var teste = ex;
                return View();
            }
        }

        //[Authorize]
        public ActionResult UploadImageItem(long id)
        {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

        [HttpPost]
        //[Authorize]
        public ActionResult UploadImageItem(HttpPostedFileBase file, string id, FormCollection collection)
        {
            CategoryItem categoryItem = null;
            if (file != null && file.ContentLength > 0)
            {
                categoryItem = _categoryItemDao.Get(Convert.ToInt64(id));

                this.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), categoryItem.Category.CategoryName));
                this.CreateFolder(Path.Combine(Server.MapPath(categoryItem.GetImagePath()), categoryItem.ItemName));

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath(categoryItem.GetImagePath()), fileName);
                file.SaveAs(path);

                UpdateCategoryItemItem(categoryItem, Path.GetFileNameWithoutExtension(file.FileName), extension);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Edit", "Category", new { id = categoryItem.Category.Id });
        }

       // [Authorize]
        public ActionResult DeleteItem(long id)
        {
            var categoryItem = _categoryItemDao.Get(id);
            return View(categoryItem);
        }

       // [Authorize]
        [HttpPost]
        public ActionResult DeleteItem(long id, CategoryItem categoryItem)
        {
            try
            {
                var refreshedCategoryItem = _categoryItemDao.Get(id);
                var categoryId = refreshedCategoryItem.Category.Id;
                _categoryItemDao.Delete(categoryItem);

                return RedirectToAction("Edit", new { id = categoryId });
            }
            catch
            {
                return View();
            }
        }

        private void UpdateCategoryItemItem(CategoryItem categoryItem, string fileName, string extension)
        {
            categoryItem.ImageName = fileName;
            categoryItem.ImageExtension = extension;
            _categoryItemDao.Update(categoryItem);
        }


        #endregion
    }
}