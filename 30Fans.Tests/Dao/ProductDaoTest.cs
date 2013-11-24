using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using _30Fans.Tests.Builder;
using Dao.Impl;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using Dao;

namespace _30Fans.Tests.Dao {
    [TestClass]
    public class ProductDaoTest : IntegrityTest{
        private ProductDao _productDao;
        private CategoryDao _categoryDao;

        [TestInitialize]
        public void Setup() {
            DeleteDatabaseIfExists();
            StartDatabase();

            _productDao = new ProductDao();
            _categoryDao = new CategoryDao();
        }

        [TestMethod]
        public void CanSave() {
            Category category = CategoryWithCategoryItem();

            _categoryDao.Save(category);
            Assert.IsNotNull(category.Id);
            Assert.IsNotNull(category.Items.FirstOrDefault().Id);

            var item = category.Items.FirstOrDefault();
            Product aNewProduct = Product().WithProductName("Produto Teste")
                                                      .WithPublishedDate(DateTime.Now)
                                                      .With(item)
                                                      .Build();
            _productDao.Save(aNewProduct);
            Assert.IsNotNull(aNewProduct.Id);
            Assert.AreEqual(1, _productDao.RowCount());
        }

        [TestMethod]
        public void CanSaveAsWellWithCategoryItem() {
            Category category = CategoryWithCategoryItem();

            _categoryDao.Save(category);
            Assert.IsNotNull(category.Id);
            Assert.IsNotNull(category.Items.FirstOrDefault().Id);

            var item = category.Items.FirstOrDefault();
            Product aNewProduct = Product().WithProductName("Produto Teste")
                                                      .WithPublishedDate(DateTime.Now)
                                                      .With(item)
                                                      .Build();
            _productDao.Save(aNewProduct);
            Assert.IsNotNull(aNewProduct.Id);
            Assert.IsNotNull(aNewProduct.CategoryItem,"Category item is null");
            Assert.AreEqual(1, _productDao.RowCount());
        }

        [TestMethod]
        public void CanSaveNewPhotos() {
            Category category = CategoryWithCategoryItem();

            _categoryDao.Save(category);
            Assert.IsNotNull(category.Id);
            Assert.IsNotNull(category.Items.FirstOrDefault().Id);

            var item = category.Items.FirstOrDefault();
            Product aNewProduct = Product().With(item)
                                           .WithPhoto("New Photo", "mengao", "jpg")
                                           .Build();

            _productDao.Save(aNewProduct);

            aNewProduct = _productDao.Get(aNewProduct.Id);

            Assert.AreEqual(1, _productDao.RowCount());
            Assert.IsNotNull(aNewProduct.Photos);
            foreach (var photo in aNewProduct.Photos) {                
                Assert.AreEqual(1, photo.Id);
            }
            
        }

        [TestMethod]
        public void CanGet() {
            Category category = CategoryWithCategoryItem();

            _categoryDao.Save(category);
            Assert.IsNotNull(category.Id);
            Assert.IsNotNull(category.Items.FirstOrDefault().Id);

            var item = category.Items.FirstOrDefault();
            Product aNewProduct = Product().WithProductName("Produto Teste")
                                                      .WithPublishedDate(DateTime.Now)
                                                      .With(item)
                                                      .Build();

            _productDao.Save(aNewProduct);
            Assert.IsNotNull(aNewProduct.Id);

            var aProduct = _productDao.Get(1);
            Assert.IsNotNull(aProduct);
        }

        [TestMethod]
        public void CanRetrieveByCategoryItemId() {
            Category category = CategoryWithCategoryItem();

            _categoryDao.Save(category);
            Assert.IsNotNull(category.Id);
            Assert.IsNotNull(category.Items.FirstOrDefault().Id);

            var item = category.Items.FirstOrDefault();
            ProductBuilder builder = Product().WithPublishedDate(DateTime.Now).With(item);

            Product aNewProduct = builder.WithProductName("Produto Teste").Build();
            Product aNewProduct2 = builder.WithProductName("Produto Teste 2").Build();

            _productDao.Save(aNewProduct);
            _productDao.Save(aNewProduct2);

            var aProductList = _productDao.GetByCategoryItemId(item.Id);
            Assert.IsNotNull(aProductList);
            Assert.AreEqual(2,aProductList.Count);
        }

        [TestMethod]
        public void CanUpdate() {
            Category category = CategoryWithCategoryItem();

            _categoryDao.Save(category);
            Assert.IsNotNull(category.Id);
            Assert.IsNotNull(category.Items.FirstOrDefault().Id);

            var item = category.Items.FirstOrDefault();
            Product aNewProduct = new ProductBuilder().WithProductName("Produto Teste")
                                                      .WithPublishedDate(DateTime.Now)
                                                      .With(item)
                                                      .Build();
            _productDao.Save(aNewProduct);
            Assert.IsNotNull(aNewProduct.Id);

            var aProduct = _productDao.Get(1);

            var valorEsperado = "Produto Alterado";
            aProduct.ProductName = valorEsperado;
            Assert.AreEqual(valorEsperado, aProduct.ProductName);
        }

        [TestMethod]
        public void CanDelete() {
            Category category = CategoryWithCategoryItem();

            _categoryDao.Save(category);
            Assert.IsNotNull(category.Id);
            Assert.IsNotNull(category.Items.FirstOrDefault().Id);

            var item = category.Items.FirstOrDefault();
            Product aNewProduct = new ProductBuilder().WithProductName("Produto Teste")
                                                      .WithPublishedDate(DateTime.Now)
                                                      .With(item)
                                                      .Build();
            _productDao.Save(aNewProduct);
            Assert.IsNotNull(aNewProduct.Id);

            var aProduct = _productDao.Get(1);
            _productDao.Delete(aProduct);

            Assert.AreEqual(0, _productDao.RowCount());
        }

        private Category CategoryWithCategoryItem() {
            return Category().WithCategoryName("Category dummy")
                             .With(CategoryItem().WithItemName("Item dummy"))
                             .Build();
        }
    }// class
}
