using System.Web;
using System.Web.Optimization;

namespace wigsboot
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {  
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.min.css",
                       "~/Content/font-awesome.min.css",
                       "~/Content/bootstrap-material-design.css",
                       "~/Content/ripples.min.css",
                       "~/Content/nga.min.css",
                       "~/Content/animate.css",
                       "~/Content/flexboxgrid.min.css",
                       "~/Content/angular-stepper.css",
                        "~/Content/ngDialog.css",
                       "~/Content/ngDialog-theme-default.css",
                       "~/Content/ngDialog-theme-plain.css",
                       "~/Content/style.css",
                       "~/Content/slides.css",
                       "~/Content/circle.css"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/myngscripts")
                .IncludeDirectory("~/Scripts/Controllers", "*.js")
                .IncludeDirectory("~/Scripts/Directives", "*.js")
                .IncludeDirectory("~/Scripts/Factories", "*.js")
                .IncludeDirectory("~/Scripts/Configuration", "*.js")
                .Include("~/Scripts/myapp.js"));

            bundles.Add(new ScriptBundle("~/bundles/ngscripts")
                .Include("~/lib/angular-touch.min.js",
                    "~/lib/angular-sanitize.min.js",
                    "~/lib/ui-bootstrap-tpls.min.js",                    
                    "~/lib/angular.js"
                ));          

            BundleTable.EnableOptimizations = true;
        }
    }
}