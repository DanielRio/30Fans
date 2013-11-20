using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace _30Fans.Tests.Domain {
    [TestClass]
    public class CategoryItemTest {
        [TestMethod]
        public void CreateCategoryItem() {
            var categoryItem = new CategoryItem();

            categoryItem.Id = 1;
            categoryItem.ItemName = "Category Item Name";
            categoryItem.Category = new Category();
            categoryItem.Enable = true;

            Assert.AreEqual(1, categoryItem.Id);
            Assert.AreEqual("Category Item Name", categoryItem.ItemName);
            Assert.IsNotNull(categoryItem.Category);
            Assert.AreEqual(true,categoryItem.Enable);
        }

        [TestMethod]
        public void ShouldHaveSpecificImageUrlPath() {
            var categoryItem = new CategoryItem();

            categoryItem.Id = 1;
            categoryItem.ItemName = "Brazil";
            categoryItem.Category = new Category() { CategoryName = "Football" };
            categoryItem.Enable = true;
            categoryItem.ImageName = "Brazil";
            categoryItem.ImageExtension = ".png";
            Assert.AreEqual("../../Content/images/categories/Football/Brazil.png", categoryItem.ImageUrl);
            Assert.AreEqual("../../Content/images/categories/Football/thumbs/Brazil.png", categoryItem.ImageThumbnailUrl);
        }
    }// class
}
