using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dao.Impl;
using Domain;
using _30Fans.Tests.Builder;

namespace _30Fans.Tests.Dao {
    [TestClass]
    public class CategoryDaoTest : IntegrityTest{
        private CategoryDao _categoryDao;
        private CategoryItemDao _categoryItemDao;
        [TestInitialize]
        public void Setup() {
            DeleteDatabaseIfExists();
            StartDatabase();

            _categoryDao = new CategoryDao();
            _categoryItemDao = new CategoryItemDao();
        }

        [TestMethod]
        public void CanSave() {
            var aNewCategory = new CategoryBuilder().WithCategoryName("New Category")
                                                    .WithImageNameAndExtension("Test", "jpg")
                                                    .WithCategoryItem(new CategoryItem() { ItemName = "Item Name" })
                                                    .Build();

            _categoryDao.Save(aNewCategory);
            Assert.AreEqual(1, _categoryDao.RowCount());
            Assert.AreEqual(1, _categoryItemDao.RowCount());
        }

        [TestMethod]
        public void CanGetByName() {
            var aNewCategory = new CategoryBuilder().WithCategoryName("New Category")
                                                     .WithImageNameAndExtension("Test", "jpg")
                                                     .WithCategoryItem(new CategoryItem() { ItemName = "Item Name" })
                                                     .Build();

            _categoryDao.Save(aNewCategory);

            var _aCategory = _categoryDao.GetByName(aNewCategory.CategoryName);
            Assert.IsNotNull(_aCategory);
            Assert.AreEqual(aNewCategory.CategoryName, _aCategory.CategoryName);
            Assert.AreEqual(aNewCategory, _aCategory);
        }
    } //class
}
