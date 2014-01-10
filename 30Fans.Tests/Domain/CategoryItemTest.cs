using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace _30Fans.Tests.Domain {
    [TestClass]
    public class CategoryItemTest : BaseDomainTest{
        [TestMethod]
        public void CreateCategoryItem() {
            var categoryItem = CategoryItem().WithId(1)
                                             .WithItemName("Brazil")
                                             .WithCategory(Category().Build())
                                             .Enabled(true)
                                             .Build();

            Assert.AreEqual(1, categoryItem.Id);
            Assert.AreEqual("Brazil", categoryItem.ItemName);
            Assert.IsNotNull(categoryItem.Category);
            Assert.AreEqual(true,categoryItem.Enable);
        }

        [TestMethod]
        public void ShouldHaveSpecificImageUrlPath() {
            var categoryItem = CategoryItem().WithImageNameAndImageExtension("Brazil",".png")
                                             .WithCategory(Category().WithCategoryName("Football").Build())                                            
                                             .Build();

            Assert.AreEqual("~/Content/images/categories/Football/Brazil.png", categoryItem.ImageUrl);
            Assert.AreEqual("~/Content/images/categories/Football/thumbs/Brazil.png", categoryItem.ImageThumbnailUrl);
        }

        [TestMethod]
        public void ShouldHaveSpecificPath() {
            var categoryItem = CategoryItem().WithCategory(Category().WithCategoryName("Football").Build())
                                             .Build();

            Assert.AreEqual("~/Content/images/categories/Football/", categoryItem.GetImagePath());
        }

        [TestMethod]
        public void WhenHasNoImageName_ImageUrlPath_ShouldHaveNoValue() {
            var categoryItem = CategoryItem().WithImageNameAndImageExtension(string.Empty, string.Empty).Build();

            Assert.AreEqual(string.Empty, categoryItem.ImageUrl);
            Assert.AreEqual(string.Empty, categoryItem.ImageThumbnailUrl);
        }

        [TestMethod]
        public void WhenHasNullImageName_ImageUrlPath_ShouldHaveNoValue() {
            var categoryItem = CategoryItem().WithImageNameAndImageExtension(null, null).Build();

            Assert.AreEqual(string.Empty, categoryItem.ImageUrl);
            Assert.AreEqual(string.Empty, categoryItem.ImageThumbnailUrl);
        }

    }// class
}
