﻿using System.Web;
using System.Web.Optimization;

namespace VCAS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/metro").Include(
                      "~/Content/site.css",
                      "~/Content/metro-all.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                      "~/Content/site-login.css"));

            bundles.Add(new StyleBundle("~/Content/form").Include(
                      "~/Content/site-form.css"));

            bundles.Add(new ScriptBundle("~/bundles/metro").Include(
                        "~/Scripts/metro.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                         "~/Scripts/chart.js"));

            bundles.Add(new ScriptBundle("~/bundles/signature_pad").Include(
                        "~/Scripts/signature_pad.min.js"));

            bundles.Add(new StyleBundle("~/Content/smartwizard").Include(
                    "~/Content/smart_wizard.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/smartwizard").Include(
                    "~/Scripts/jquery.smartWizard.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/main-index").Include(
                    "~/Scripts/main-index.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                    "~/Scripts/tinymce/jquery.tinymce.min.js",
                    "~/Scripts/tinymce/tinymce.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/phoneNumber").Include(
                    "~/Scripts/PhoneNumber.js"
                ));

            bundles.Add(new StyleBundle("~/Content/select2").Include(
                    "~/Content/css/select2.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                    "~/Scripts/select2.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                "~/Scripts/moment.js",
                "~/Scripts/fullcalendar*"));

            bundles.Add(new StyleBundle("~/Content/fullcalendar").Include(
                "~/Content/fullcalendar.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
