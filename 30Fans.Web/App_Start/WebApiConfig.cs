using System.Web;
using System.Web.Optimization;
using System.Web.Http;

namespace _30Fans.Web
{
    class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}
