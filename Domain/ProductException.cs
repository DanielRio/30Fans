using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public class ProductNotAvailableException : Exception{
        public ProductNotAvailableException(string message) : base(message) { 

        }
    } // class

    public class ProductNotFoundException : Exception {
        public ProductNotFoundException(string message)
            : base(message) {

        }
    } // class

    public class ProductDisabledException : Exception {
        public ProductDisabledException(string message)
            : base(message) {

        }
    } // class

    public class PhotoException : Exception {
        public PhotoException(string message)
            : base(message) {

        }
    } // class
}
