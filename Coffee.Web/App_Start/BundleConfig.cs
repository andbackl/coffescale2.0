using System.Web.Optimization;

namespace Coffee.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/bundle").Include(
                "~/Content/css/bootstrap.css", 
                "~/Content/css/site.css"));

            bundles.Add(new ScriptBundle("~/js/bundle").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.signalR-2.0.0-rc1.js"));            
        }
    }
}