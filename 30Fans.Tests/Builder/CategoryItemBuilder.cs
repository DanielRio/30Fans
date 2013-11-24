using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace _30Fans.Tests.Builder {
    public class CategoryItemBuilder {
        private CategoryItem _categoryItem;

        public CategoryItemBuilder() {
            _categoryItem = new CategoryItem();
        }

        public CategoryItem Build() {
            return _categoryItem;
        }

        public CategoryItemBuilder WithId(long id) {
            _categoryItem.Id = id;
            return this;
        }

        public CategoryItemBuilder WithItemName(string itemName) {
            _categoryItem.ItemName = itemName;
            return this;
        }

        public CategoryItemBuilder WithImageNameAndImageExtension(string imageName, string extension) {
            _categoryItem.ImageName = imageName;
            _categoryItem.ImageExtension = extension;
            return this;
        }

        public CategoryItemBuilder WithCategory(Category category) {
            _categoryItem.Category = category;
            return this;
        }

        public CategoryItemBuilder Enabled(bool enabled) {
            _categoryItem.Enable = enabled;
            return this;
        }
    } //class
}
