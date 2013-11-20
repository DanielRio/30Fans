using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace _30Fans.Misc {
    public static class Helper {
        public static IHtmlString ConvertBooleanToYesNo(this HtmlHelper htmlHelper, bool value) {
            return new HtmlString(value ? "Yes" : "No");
        }
    } //class
}
