using System.Web.Mvc;
using System.Web.Routing;

namespace Netsy.DemoWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    "Default", // Route name
                    "{action}", // URL with parameters
                    new { controller = "Home", action = "Front" } // Parameter defaults
                    );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}