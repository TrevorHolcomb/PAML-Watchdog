using System.Web;
using System.Web.Optimization;

namespace AdministrationPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-tokenfield").Include(
                        "~/Scripts/bootstrap-tokenfield.js"));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap-tokenfield").Include(
                        "~/Content/bootstrap-tokenfield/bootstrap-tokenfield.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Styles/css/bootstrap.css",
                      "~/Styles/css/site.css",
                      "~/Content/font-awesome/css/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/css/userPortal").Include(
                      "~/Styles/css/bootstrap.css",
                      "~/Styles/css/UserPortalSite.css",
                      "~/Content/font-awesome/css/font-awesome.css"));
        }
    }
}
