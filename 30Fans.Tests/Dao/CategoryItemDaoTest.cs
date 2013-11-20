using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _30Fans.Tests.Builder;
using Dao.Impl;

namespace _30Fans.Tests.Dao {
    [TestClass]
    public class CategoryItemDaoTest : IntegrityTest {
        private CategoryItemDao _categoryItemDao;
        private CategoryDao _categoryDao;

        [TestInitialize]
        public void Setup() {
            DeleteDatabaseIfExists();
            StartDatabase();

            _categoryItemDao = new CategoryItemDao();
            _categoryDao = new CategoryDao();
        }

        [TestMethod]
        public void SaveCategoryItem() {
            var categoryItem = new CategoryItemBuilder().WithItemName("Category item Test").Build();
            var category = new CategoryBuilder().WithCategoryName("Category Test").WithCategoryItem(categoryItem).Build();
            _categoryDao.Save(category);
            Assert.AreEqual(1, _categoryDao.RowCount());
            Assert.AreEqual(1, _categoryItemDao.RowCount());           
        }

        [TestMethod]
        public void UpdateCategoryItem() {
            var categoryItem = new CategoryItemBuilder().WithItemName("Category item Test").Build();
            var category = new CategoryBuilder().WithCategoryName("Category Test").WithCategoryItem(categoryItem).Build();
            _categoryDao.Save(category);
            Assert.AreEqual(1, _categoryDao.RowCount());
            Assert.AreEqual(1, _categoryItemDao.RowCount());
            Assert.IsFalse(category.Items[0].Enable);

            categoryItem = _categoryItemDao.Get(categoryItem.Id);
            categoryItem.Enable = true;
            _categoryItemDao.Update(categoryItem);

            categoryItem = _categoryItemDao.Get(categoryItem.Id);
            Assert.IsTrue(categoryItem.Enable);
        }
    } //class
}
