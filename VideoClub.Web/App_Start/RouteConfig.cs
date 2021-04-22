using System.Web.Mvc;
using System.Web.Routing;

namespace VideoClub.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default_Areas",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "movies", action = "index", id = UrlParameter.Optional }
            ).DataTokens.Add("area", "movies");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "movies", action = "index", id = UrlParameter.Optional }
            );

        }
    }
}
