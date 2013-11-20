using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public abstract class Show {
        public virtual string ImageUrl { get; set; }
        public virtual string ImageThumbnailUrl { get; set; }
    }
}
