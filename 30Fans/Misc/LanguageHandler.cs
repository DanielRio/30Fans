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
            string currentlanguage = GetSelectedLanguage();
            ApplyCurrentLanguage(currentlanguage);                
        }

        private string GetSelectedLanguage() {
            HttpContext context = HttpContext.Current;
            string selectedLaguage = string.Empty;
            if (context.Session != null && context.Session["currentLanguage"] != null) {
                selectedLaguage = GetUserChoiceLanguage();
            } else {
                selectedLaguage = GetBrowserLanguage();
            }
            return selectedLaguage;
        }

        private string GetUserChoiceLanguage() {
            return (string)HttpContext.Current.Session["currentLanguage"];
        }

        private string GetBrowserLanguage() {
            var clientLanguages = HttpContext.Current.Request.UserLanguages;
            if (clientLanguages.Count() > 0) {
                return clientLanguages[0];
            }
            return null;
        }
        private void ApplyCurrentLanguage(string currentLanguage) {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(currentLanguage);
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(currentLanguage);
        }
    } //class
}