using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain {
    public class Category : IConfiguration{
        public virtual long Id { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual bool Enable { get; set; }
        public virtual int Priority { get; set; }
        public virtual string ImageName { get; set; }
        public virtual string ImageExtension { get; set; }
        public virtual IList<CategoryItem> Items { get; set; }

        public virtual string FullImageName {
            get { return this.ImageName + this.ImageExtension; }
        }

        public virtual string ImageUrl {
            get {
                return string.Format("{0}/{1}{2}", "../../Content/images/categories", this.ImageName, this.ImageExtension);
            }
        }

        public virtual string ImageThumbnailUrl {
            get {
                return string.Format("{0}/{1}/{2}{3}", "../../Content/images/categories", "thumbs", this.ImageName, this.ImageExtension);
            }
        }

        public override bool Equals(object obj) {
            if (obj == null || !obj.GetType().Equals(typeof(Category)))
                return false;    
            
            var category = (Category)obj;
            return category.Id == this.Id;
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

    }  // class
}
