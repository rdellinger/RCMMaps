using System.Web;
using System.Web.Optimization;

namespace RCMMaps
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/RCMMaps.css"));

            // My custom CSS and JS
            bundles.Add(new ScriptBundle("~/bundles/RCMMaps").Include(
                      "~/Scripts/tabHash.js"));

            bundles.Add(new ScriptBundle("~/bundles/Home").Include(
                      "~/Scripts/accordion.js",
                      "~/Scripts/jquery-1.10.2.min.js",
                      "~/Scripts/jquery.transform.js",
                      "~/Scripts/jquery.easing.1.3.js",
                      "~/Scripts/redcomma.js",
                      "~/Scripts/detectmobilebrowser.js"));

            bundles.Add(new StyleBundle("~/Content/Home").Include(
                      "~/Content/accordion.css",
                      "~/Content/RedComma.css"));

            bundles.Add(new StyleBundle("~/Content/Mobile").Include(
                      "~/Content/RedCommaMobile.css",
                      "~/Content/RedCommaTheme.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
