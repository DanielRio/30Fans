using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public class Product : IConfiguration{
        public virtual long Id { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int AvailableQuantity { get; set; }
        public virtual string PaymentCode { get; set; }
        public virtual bool Enable { get; set; }
        public virtual int Priority { get; set; }
        public virtual string ImageName { get; set; }
        public virtual string ImageExtension { get; set; }
        public virtual CategoryItem CategoryItem { get; set; }
        public virtual DateTime PublishedDate { get; set; }

        private IList<Photo> photos;
        public virtual IEnumerable<Photo> Photos {
            get { return photos; }
        }

        public virtual string ImageUrl {
            get {
                if (string.IsNullOrEmpty(ImageName))
                    return string.Empty;
                return string.Format("{0}{1}{2}", GetImagePath(), this.ImageName, this.ImageExtension);
            }
        }

        public virtual string GetImagePath() {
            return string.Format("{0}/{1}/{2}/", "../../Content/images/categories", this.CategoryItem.Category.CategoryName, this.CategoryItem.ItemName);
        }

        public virtual void AddPhoto(string text, string photoName, string extension) {
            if (AvailableQuantity == 0)
                throw new ProductNotAvailableException("Product not available");
            if (!Enable)
                throw new ProductDisabledException("Product disabled");
            if (photos == null)
                photos = new List<Photo>();

            photos.Add(new Photo(this, text, photoName, extension));
            AvailableQuantity--;
        } 
    } // class
}
