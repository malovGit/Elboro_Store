using System.Web.Mvc;
using System.Web.Routing;

namespace ASPNETIdentityWithOnion {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Store", action = "Index",
                    id = UrlParameter.Optional },
                namespaces: new[] { "ASPNETIdentityWithOnion.Web.Controllers" }
            );
            //routes.MapRoute(
            //    name: "Products",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Product", action = "ProductList", id = UrlParameter.Optional },
            //    namespaces: new[] { "ASPNETIdentityWithOnion.Controllers" }
            //    );
        }
    }
}