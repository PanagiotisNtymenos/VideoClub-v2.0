using System.Web.Mvc;

namespace VideoClub.Web.Areas.Rentings
{
    public class RentingsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Rentings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Rentings_default",
                "rentings/{controller}/{action}/{id}",
                new { action = "index", id = UrlParameter.Optional },
                new[] { "VideoClub.Web.Areas.Rentings.Controllers" }
            );
        }
    }
}