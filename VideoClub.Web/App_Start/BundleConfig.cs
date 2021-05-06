using System.Web.Optimization;

namespace VideoClub.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                      "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/selectize").Include(
                      "~/Scripts/selectize.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                      "~/Scripts/bootbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/scripts.js"));

            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include(
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui.structure.css",
                      "~/Content/jquery-ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/selectize").Include(
                       "~/Content/selectize.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/flatly.bootstrap.css",
                       "~/Content/site.css"));

        }
    }
}
