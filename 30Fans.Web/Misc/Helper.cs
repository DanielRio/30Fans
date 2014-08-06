using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace _30Fans.Web.Misc
{
    public static class Helper {
        public static IHtmlString ConvertBooleanToYesNo(this HtmlHelper htmlHelper, bool value) {
            return new HtmlString(value ? "Yes" : "No");
        }

        public static IHtmlString HomeActionLink(this HtmlHelper helper, string text, object htmlAttributes) {
            var url = new UrlHelper(helper.ViewContext.RequestContext);

            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var homeAnchorBuilder = new TagBuilder("a");
            homeAnchorBuilder.MergeAttribute("href", url.Action(string.Empty, "Home", null));
            homeAnchorBuilder.MergeAttributes(new RouteValueDictionary(attributes));

            string anchorHtml = homeAnchorBuilder.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(anchorHtml);
        }

        public static String GetSelectedLanguage(this HtmlHelper helper){
            LanguageHandler handler = new LanguageHandler();
            return handler.GetSelectedLanguage();
        }

    } //class
}
