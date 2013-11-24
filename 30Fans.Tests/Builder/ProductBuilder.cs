using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace _30Fans.Tests.Builder {
    public class ProductBuilder {
        private Product product;
        public ProductBuilder() {
            product = new Product();
            product.AvailableQuantity = 30;
            product.PublishedDate = DateTime.Now;
            product.ProductName = "Default Product Name";
            product.CategoryItem = new CategoryItemBuilder().Build();
            product.ImageName = string.Empty;
            product.ImageExtension = string.Empty;
            product.Enable = true;
        }

        public Product Build() {
            return product;
        }

        public ProductBuilder WithId(long id) {
            product.Id = id;
            return this;
        }

        public ProductBuilder WithProductName(string name) {
            product.ProductName = name;
            return this;
        }

        public ProductBuilder WithImageName(string imageName) {
            product.ImageName = imageName;
            return this;
        }

        public ProductBuilder WithImageExtension(string imageExtension) {
            product.ImageExtension = imageExtension;
            return this;
        }

        public ProductBuilder WithPublishedDate(DateTime date) {
            product.PublishedDate = date;
            return this;
        }

        public ProductBuilder With(CategoryItem categoryItem) {
            product.CategoryItem = categoryItem;
            return this;
        }

        public ProductBuilder WithPhoto(string photoText,string photoName, string extension) {
            product.AddPhoto(photoText, photoName, extension);
            return this;
        }

        public ProductBuilder WithNoAvailableSlots() {
            product.AvailableQuantity = 0;
            return this;
        }

        public ProductBuilder WithAvailableSlots(int numberAvailableSlots) {
            product.AvailableQuantity = numberAvailableSlots;
            return this;
        }

        public ProductBuilder Disabled() {
            product.Enable = false;
            return this;
        }
    }// class
}
