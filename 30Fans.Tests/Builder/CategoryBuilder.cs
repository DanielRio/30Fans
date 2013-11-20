using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace _30Fans.Tests.Builder {
    public class CategoryBuilder {
        private Category category;

        public CategoryBuilder() {
            category = new Category();
        }

        public Category Build() {
            return category;
        }

        public CategoryBuilder WithId(long id) {
            category.Id = id;
            return this;
        }

        public CategoryBuilder WithCategoryName(string categoryName) {
            category.CategoryName = categoryName;
            return this;
        }

        public CategoryBuilder WithImageNameAndExtension(string imageName, string extension) {
            category.ImageName = imageName;
            category.ImageExtension = extension;
            return this;
        }

        public CategoryBuilder WithCategoryItem(CategoryItem categoryItem) {
            if (category.Items == null)
                category.Items = new List<CategoryItem>();
            categoryItem.Category = category;
            category.Items.Add(categoryItem);
            return this;
        }
    } //class
}
