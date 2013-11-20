using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain {
    public interface IConfiguration {
        bool Enable { get; set; }
        int Priority { get; set; }
    }
}
