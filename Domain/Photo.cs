using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public class Photo {
        private Product product;

        protected Photo() { }
        public Photo(Product product, string text, string photoName, string extension) {
            this.product = product;
            this.Text = text;
            this.PhotoName = photoName;
            this.Extension = extension;
        }   

        public virtual string ImageThumbnailUrl {
            get {
                //return string.Format("{0}/{1}/{2}/{3}/{4}/{5}{6}", "../../Content/images/categories", this.product.CategoryItem.Category.CategoryName, this.product.CategoryItem.ItemName, this.product.ProductName, "thumbs", this.Id, this.Extension);
                return ImageUrl;
            }
        }

        public virtual string ImageUrl {
            get {
                return string.Format("{0}/{1}/{2}/{3}/{4}{5}", "../../Content/images/categories", this.product.CategoryItem.Category.CategoryName, this.product.CategoryItem.ItemName, this.product.ProductName, this.Id, this.Extension);
            }
        }

        public virtual Product Product {
            get { return product; }
            set { product = value; }
        }

        public virtual long? Id { get; set; }
        public virtual string Text { get; set; }
        public virtual string PhotoName { get; set; }
        public virtual string Extension { get; set; }
    }// class
}
