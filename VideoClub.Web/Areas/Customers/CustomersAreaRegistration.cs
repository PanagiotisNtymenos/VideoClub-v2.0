using System.Web.Mvc;

namespace VideoClub.Web.Areas.Customers
{
    public class CustomersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Customers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Customers_default",
                "customers/{action}/{id}",
                new { controller = "customers", action = "index", id = UrlParameter.Optional }
            );

        }
    }
}