[assembly: WebActivator.PreApplicationStartMethod(typeof(_30Fans.App_Start.Combres), "PreStart")]
namespace _30Fans.App_Start {
	using System.Web.Routing;
	using global::Combres;
	
    public static class Combres {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}