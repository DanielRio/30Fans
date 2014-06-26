using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_30Fans.Web.Startup))]
namespace _30Fans.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
