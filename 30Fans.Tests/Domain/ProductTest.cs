using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using _30Fans.Tests.Builder;

namespace _30Fans.Tests.Domain {
    [TestClass]
    public class ProductTest {
        [TestMethod]
        public void CreateProduct() {
            var aCategory = new CategoryBuilder().WithCategoryName("Football")
                                                 .WithCategoryItem(new CategoryItemBuilder().WithItemName("Brazil").Build())
                                                 .Build();

            string expectedValue = "Flamengo";
            Product product = new Product();
            product.CategoryItem = aCategory.Items.FirstOrDefault();
            product.ProductName = expectedValue;
            product.PaymentCode = "51PUG9GE5UZBU";
            product.ImageName = "Flamengo";
            product.ImageExtension = "jpg";

            Assert.AreEqual(expectedValue, product.ProductName);
            Assert.AreEqual("../../Content/images/categories/Football/Brazil/Flamengo.jpg", product.ImageUrl);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ProductNotAvailableException))]
        public void AddPhotoNoAvailableSlotProduct() {
            var aSoldOutProduct = new ProductBuilder().WithNoAvailableSlots().Build();
            aSoldOutProduct.AddPhoto(null,null,null);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductDisabledException))]
        public void AddPhotoDisabledProduct() {
            var aSoldOutProduct = new ProductBuilder().Disabled().Build();
            aSoldOutProduct.AddPhoto(null, null, null);
        }

        [TestMethod]
        public void AddPhoto() {
            var aSoldOutProduct = new ProductBuilder().WithAvailableSlots(10).Build();
            aSoldOutProduct.AddPhoto(null,null,null);
            aSoldOutProduct.AddPhoto(null,null,null);

            Assert.AreEqual(8, aSoldOutProduct.AvailableQuantity);
        }

        [TestMethod]
        public void CreatePhoto() {
            var aCategory = new CategoryBuilder().WithCategoryName("Football")
                                                 .WithCategoryItem(new CategoryItemBuilder().WithItemName("Brazil").Build())
                                                 .Build();

            var aProduct = new ProductBuilder().WithProductName("Flamengo").Build();
            aProduct.CategoryItem = aCategory.Items.FirstOrDefault();
            aProduct.AddPhoto("photo test", "mengao", "jpg");

            Assert.AreEqual(1, aProduct.Photos.Count());

            foreach ( var photo in aProduct.Photos){
                Assert.AreEqual("../../Content/images/categories/Football/Brazil/Flamengo/.jpg", photo.ImageUrl);
                Assert.AreEqual("../../Content/images/categories/Football/Brazil/Flamengo/thumbs/.jpg", photo.ImageThumbnailUrl);
            }
            
        }
    }
}//class
