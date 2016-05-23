using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Harcourts.Face.Website
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/staticcontents/js").Include(
                // JQuery
                "~/Scripts/jquery-{version}.js",
                // Plupload
                "~/StaticResources/plupload-2.1.9/js/moxie.min.js",
                "~/StaticResources/plupload-2.1.9/js/plupload.min.js"
                ));

            bundles.Add(new StyleBundle("~/staticcontents/css").Include(
                "~/StaticResources/css/site.css"
                ));
        }
    }
}