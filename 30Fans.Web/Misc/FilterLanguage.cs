using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _30Fans.Web.Misc
{
    public class InternationalizationAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            ILanguageHandler languageHandler = new LanguageHandler();
            languageHandler.ApplyCurrentLanguage();
            base.OnActionExecuting(filterContext);
        }
    }
}