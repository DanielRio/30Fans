using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public class NotFoundProduct : Product{
        public override int AvailableQuantity {
            get {
                return 0;
            }
            set {
                base.AvailableQuantity = 0;
            }
        }

        public override string ImageUrl {
            get {
                return string.Empty;
            }
        }


    } //class
}
