using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace _30Fans.Tests.Domain {
    [TestClass]
    public class CategoryTest {
        [TestMethod]
        public void CreateCategory() {
            var category = new Category();
            category.Id = 1;
            category.CategoryName = "Categoria 1";
            category.ImageName = "teste";
            category.ImageExtension = ".jpg";
            category.Enable = true;
            category.Items = new List<CategoryItem>();
            category.Items.Add(new CategoryItem());

            Assert.AreEqual("Categoria 1", category.CategoryName);
            Assert.AreEqual("teste.jpg", category.FullImageName);
            Assert.AreEqual(true, category.Enable);
            Assert.AreEqual(1,category.Items.Count);
        }

        [TestMethod]
        public void ShouldHaveSpecificUrlPath() {
            var category = new Category();
            category.Id = 1;
            category.CategoryName = "Running";
            category.ImageName = "Running";
            category.ImageExtension = ".jpg";

            Assert.AreEqual("../../Content/images/categories/Running.jpg", category.ImageUrl);
            Assert.AreEqual("../../Content/images/categories/thumbs/Running.jpg", category.ImageThumbnailUrl);
        }
    }// class
}
