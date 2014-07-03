namespace WebEndpoint
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Libs")
                .IncludeDirectory("~/Scripts", "*.js"));
        }
    }
}