using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public class CategoryItem : IConfiguration{
        public virtual long Id{ get; set; }
        public virtual string ItemName { get; set; }
        public virtual Category Category { get; set; }
        public virtual bool Enable { get; set; }
        public virtual string ImageName { get; set; }
        public virtual string ImageExtension { get; set; }
        public virtual int Priority { get; set; }

        public virtual string FullImageName {
            get { return this.ImageName + this.ImageExtension; }
        }

        public virtual string ImageUrl {
            get {
                return string.Format("{0}/{1}/{2}{3}", "../../Content/images/categories", this.Category.CategoryName, this.ImageName, this.ImageExtension);
            }
        }

        public virtual string ImageThumbnailUrl {
            get {
                return string.Format("{0}/{1}/{2}/{3}{4}", "../../Content/images/categories", this.Category.CategoryName, "thumbs", this.ImageName, this.ImageExtension);
            }
        }
    }// class
}
