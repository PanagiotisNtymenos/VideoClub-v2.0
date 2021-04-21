using System.Web.Mvc;

namespace VideoClub.Web.Areas.Movies
{
    public class MoviesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Movies";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Movies_default",
                "movies/{controller}/{action}/{id}",
                new { action = "index", id = UrlParameter.Optional }
            );
        }
    }
}