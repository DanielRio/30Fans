using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public class NullCategory : Category{
        public override string CategoryName {
            get {
                return "Categoria Sem Nome";
            }
            set {
                base.CategoryName = value;
            }
        }

        public override IList<CategoryItem> Items {
            get {
                return new List<CategoryItem>();
            }
            set {
                base.Items = value;
            }
        }
    }// class
}
