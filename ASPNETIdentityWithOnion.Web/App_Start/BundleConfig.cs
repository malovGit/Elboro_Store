using Forloop.HtmlHelpers;
using System.Web.Optimization;

namespace ASPNETIdentityWithOnion
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                       "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                       "~/Scripts/jquery-ui-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymigrate").Include(
                        "~/Scripts/jquery-migrate*"));
            //EShoper
            //bundles.Add(new ScriptBundle("~/Eshopper/prettyPhoto").Include(
            //   "Scripts/EShopper/jquery.prettyPhoto.js"));

            //bundles.Add(new ScriptBundle("~/Eshopper/scrollUp").Include(
            //   "Scripts/EShopper/jquery.scrollUp.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/twitter-bootstrap-hover-dropdown.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
               "~/media/js/jquery.dataTables.min.js",
               "~/media/js/dataTables.bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/SiteScripts").Include(
                "~/Scripts/SiteScripts/CategoryMenu.js",
                "~/Scripts/SiteScripts/SiteJs.js"));

            bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                    //"~/Scripts/prototype.js",
                    "~/Scripts/lightbox.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scriptaculous").Include(
                    //"~/Scripts/Scriptaculous/builder.js",
                    //"~/Scripts/Scriptaculous/controls.js",
                    //"~/Scripts/Scriptaculous/dragdrop.js",
                    "~/Scripts/Scriptaculous/effects.js",
                    "~/Scripts/Scriptaculous/scriptaculous.js"
                    //"~/Scripts/Scriptaculous/slider.js",
                    //"~/Scripts/Scriptaculous/sound.js",
                    //"~/Scripts/Scriptaculous/unittest.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/slick").Include(                 
                   "~/Scripts/slick.min.js"));

            //*****************
            //Dashboard Scripts
            //*****************

            //Common Scripts for all pages
            bundles.Add(new ScriptBundle("~/bundles/assets").Include(
                "~/Content/assets/js/jquery.dcjqaccordion.2.7.js",
                "~/Content/assets/js/jquery.scrollTo.min.js",
                "~/Content/assets/js/jquery.nicescroll.js",
                "~/Content/assets/js/common-scripts.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/assets2").Include(
                "~/Content/assets/js/bootstrap-switch.js",
                "~/Content/assets/js/jquery.tagsinput.js",
                "~/Content/assets/js/bootstrap-datepicker/js/bootstrap-datepicker.js",
                "~/Content/assets/js/bootstrap-daterangepicker/date.js",
                "~/Content/assets/js/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/assets/js/bootstrap-inputmask/bootstrap-inputmask.min.js",
                 "~/Content/assets/js/form-component.js",
                 "~/Content/assets/js/gritter/js/jquery.gritter.js"
                ));


            ScriptContext.ScriptPathResolver = Scripts.Render;

       
            //"~/Content/Site.css"
            //"~/Content/css/font-awesome-ie7.css",

            bundles.Add(new StyleBundle("~/Content/EShope").Include(
                      "~/Content/EShope/responsive.css",
                      "~/Content/EShope/bootstrap.min.css",
                      "~/Content/EShope/font-awesome.min.css",
                      "~/Content/EShope/animate.css",
                      "~/Content/EShope/main.css",
                      "~/Content/EShope/prettyPhoto.css",
                      "~/Content/EShope/price-range.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/StoreStyle").Include(
                "~/Content/StoreStyle/Breadcramb.css",
                "~/Content/StoreStyle/SearchMenu.css",
                "~/Content/StoreStyle/CategoryMenu.css"
                /*"~/Content/StoreStyle/CategoryBodyStyle.css"*/));

            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                //"~/media/css/jquery.dataTables.min.css"
                "~/media/css/dataTables.bootstrap.min.css"
                ));
            bundles.Add(new StyleBundle("~/Dashboard/css").Include(
                      "~/Content/DashboardStyle/CloseImage.css"));

            bundles.Add(new StyleBundle("~/Content/bootshop").Include(
                "~/Content/themes/js/google-code-prettify/prettify.css",
                "~/Content/themes/css/base.css",
                "~/Content/Bootshop/CouruselSlide.css",
                "~/Content/themes/switch/themeswitch.css"));

            bundles.Add(new StyleBundle("~/Content/lightbox").Include(
                     "~/Content/css/lightbox.min.css"));

            bundles.Add(new StyleBundle("~/Content/slick").Include(
                    "~/Content/css/slick.css",
                    "~/Content/css/slick-theme.css"));

            bundles.Add(new StyleBundle("~/Content/Theme/base/css").Include(
                        "~/Content/Theme/base/jquery.ui.core.css",
                        "~/Content/Theme/base/jquery.ui.resizable.css",
                        "~/Content/Theme/base/jquery.ui.selectable.css",
                        "~/Content/Theme/base/jquery.ui.accordion.css",
                        "~/Content/Theme/base/jquery.ui.autocomplete.css",
                        "~/Content/Theme/base/jquery.ui.button.css",
                        "~/Content/Theme/base/jquery.ui.dialog.css",
                        "~/Content/Theme/base/jquery.ui.slider.css",
                        "~/Content/Theme/base/jquery.ui.tabs.css",
                        "~/Content/Theme/base/jquery.ui.datepicker.css",
                        "~/Content/Theme/base/jquery.ui.progressbar.css",
                        "~/Content/Theme/base/jquery.ui.theme.css"));

            //****************
            //DASHBOARD Styles
            //****************
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/css/bootstrap-responsive.min.css",
                 "~/Content/css/font-awesome.css"
                 ));

            bundles.Add(new StyleBundle("~/Content/assets").Include(
                "~/Content/assets/js/bootstrap-datepicker/css/datepicker.css",
                "~/Content/assets/js/bootstrap-daterangepicker/daterangepicker.css",
                "~/Content/assets/css/style.css",
                "~/Content/assets/css/style-responsive.css",
                "~/Content/assets/css/table-responsive.css",
                "~/Content/assets/css/to-do.css",
                "~/Content/assets/js/gritter/css/jquery.gritter.css"));
        }

    }
}
