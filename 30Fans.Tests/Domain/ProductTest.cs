using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace _30Fans.Tests.Domain {
    [TestClass]
    public class ProductTest : BaseDomainTest{

        [TestMethod]
        public void CreateProduct() {
            var aCategory = Category().WithCategoryName("Football")
                                      .With(CategoryItem().WithItemName("Brazil"))
                                      .Build();

            string expectedValue = "Flamengo";

            Product aProduct = Product().With(aCategory.Items.FirstOrDefault())
                                        .WithProductName(expectedValue)
                                        .WithImageName("Flamengo")
                                        .WithImageExtension(".jpg")
                                        .Build();

            Assert.AreEqual(expectedValue, aProduct.ProductName);
            Assert.AreEqual("~/Content/images/categories/Football/Brazil/Flamengo.jpg", aProduct.ImageUrl);
        }

        [TestMethod]
        public void WhenHasNoImageName_ImageUrlPath_ShouldHaveNoValue() {
            var product = Product().WithImageName(string.Empty).WithImageExtension(string.Empty).Build();

            Assert.AreEqual(string.Empty, product.ImageUrl);
        }

        [TestMethod]
        public void WhenHasNullImageName_ImageUrlPath_ShouldHaveNoValue() {
            var product = Product().WithImageName(null).WithImageExtension(null).Build();

            Assert.AreEqual(string.Empty, product.ImageUrl);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ProductNotAvailableException))]
        public void AddPhotoNoAvailableSlotProduct() {
            var aSoldOutProduct = Product().WithNoAvailableSlots().Build();
            aSoldOutProduct.AddPhoto(null,null,null);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductDisabledException))]
        public void AddPhotoDisabledProduct() {
            var aSoldOutProduct = Product().Disabled().Build();
            aSoldOutProduct.AddPhoto(null, null, null);
        }

        [TestMethod]
        public void AddPhoto() {
            var aSoldOutProduct = Product().WithAvailableSlots(10).Build();
            aSoldOutProduct.AddPhoto(null,null,null);
            aSoldOutProduct.AddPhoto(null,null,null);

            Assert.AreEqual(8, aSoldOutProduct.AvailableQuantity);
        }

        [TestMethod]
        [Ignore()]
        [Description("Verify image path")]
        public void CreatePhoto() {
            var aCategory = Category().WithCategoryName("Football")
                                                 .With(CategoryItem().WithItemName("Brazil"))
                                                 .Build();

            var aProduct = Product().WithProductName("Flamengo")
                                    .With(aCategory.Items.FirstOrDefault())
                                    .WithPhoto("photo test", "mengao", "jpg")
                                    .Build();

            Assert.AreEqual(1, aProduct.Photos.Count());

            foreach ( var photo in aProduct.Photos){
                Assert.AreEqual("../../Content/images/categories/Football/Brazil/Flamengo/.jpg", photo.ImageUrl);
                Assert.AreEqual("../../Content/images/categories/Football/Brazil/Flamengo/thumbs/.jpg", photo.ImageThumbnailUrl);
            }            
        }

        [TestMethod]
        [ExpectedException(typeof(PhotoException))]
        public void GetLastSavedPhoto_When_There_Are_No_Photo() {
            var aProduct = Product().Build();
            aProduct.GetLastSavedPhoto();
        }

        [TestMethod]
        public void GetLastSavedPhoto_When_There_Are_Photo_Saved() {
            var aCategory = Category().WithCategoryName("Football")
                                                 .With(CategoryItem().WithItemName("Brazil"))
                                                 .Build();
            var aProduct = Product().WithPhoto("mengao", "mengo", ".jpg").With(aCategory.Items.FirstOrDefault()).Build();
            
            var photo = aProduct.GetLastSavedPhoto();
            Assert.IsNull(photo);
        }

        [TestMethod]
        public void ShouldHaveSpecificPath() {
            var aCategory = Category().WithCategoryName("Football")
                                                 .With(CategoryItem().WithItemName("Brazil"))
                                                 .Build();

            var aProduct = Product().WithProductName("Flamengo")
                                    .With(aCategory.Items.FirstOrDefault())
                                    .WithPhoto("photo test", "mengao", "jpg")
                                    .Build();

            Assert.AreEqual("~/Content/images/categories/Football/Brazil/", aProduct.GetImagePath());
        }

        [TestMethod]
        public void ShouldHaveSpecificPhotoImagePath() {
            var aCategory = Category().WithCategoryName("Football")
                                                 .With(CategoryItem().WithItemName("Brazil"))
                                                 .Build();

            var aProduct = Product().WithProductName("Flamengo")
                                    .With(aCategory.Items.FirstOrDefault())
                                    .WithPhoto("photo test", "mengao", "jpg")
                                    .Build();

            Assert.AreEqual("~/Content/images/categories/Football/Brazil/Flamengo/", aProduct.GetPhotoImagePath());
        }
    }
}//class
