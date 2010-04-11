using System.Web.Mvc;
using System.Web.Routing;

namespace Netsy.DemoWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Category", "Category/{category}", 
                new { controller = "Home", Action = "Category", category = DefaultDataConstants.DefaultCategory });
            routes.MapRoute("Color", "Color/{color}", 
                new { controller = "Home", Action = "Color", color = DefaultDataConstants.DefaultColor });
            routes.MapRoute("Shop", "Shop/{shop}", 
                new { controller = "Home", Action = "Shop", shop = DefaultDataConstants.DefaultShop } );
            routes.MapRoute("Favorites", "Favorites/{shop}", 
                new { controller = "Home", Action = "Favorites", shop = DefaultDataConstants.DefaultShop });

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