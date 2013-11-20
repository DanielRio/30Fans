using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;

namespace _30Fans.Misc {
    public class LanguageHandler : ILanguageHandler{
        public void SetLanguage(string language) {
            HttpContext.Current.Session["currentLanguage"] = language;
        }

        public void ApplyCurrentLanguage() {
            HttpContext context = HttpContext.Current;
            if (context.Session != null && context.Session["currentLanguage"] != null) {
                string currentlanguage = (string)HttpContext.Current.Session["currentLanguage"];

                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(currentlanguage);
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(currentlanguage);
            }
        }
    } //class
}