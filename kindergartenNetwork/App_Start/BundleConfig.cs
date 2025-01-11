using System.Web;
using System.Web.Optimization;

namespace kindergartenNetwork
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/default/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/default/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/default/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/default/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region CSS      
            #region Metronic GLOBAL MANDATORY STYLES
            bundles.Add(new StyleBundle("~/GlobalMandotaryRTL/css").Include(
                      //"~/Content/css/fontCss.css",
                      "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.css",
                      //"~/Content/assets/global/plugins/uniform/css/uniform.default.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap-rtl.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch-rtl.min.css",
                      //"~/Content/assets/global/plugins/bootstrap-toastr/toastr-rtl.min.css",

                      "~/Content/assets/global/plugins/jquery-notific8/jquery.notific8.min.css",
                      "~/Content/assets/global/plugins/bootstrap-select/css/bootstrap-select-rtl.css"
                      ));
            #endregion
            #region Metronic THEME GLOBAL STYLES
            bundles.Add(new StyleBundle("~/GlobalstyleRTL/css").Include(
                      "~/Content/assets/global/css/components-rtl.min.css",
                      "~/Content/assets/global/css/plugins-rtl.min.css"));
            #endregion
            #region Metronic THEME LAYOUT STYLES
            bundles.Add(new StyleBundle("~/ThemLayoutRTL/css").Include(
                      "~/Content/assets/layouts/layout/css/layout-rtl.min.css",
                      "~/Content/assets/layouts/layout/css/themes/grey-rtl.min.css",
                      "~/Content/assets/layouts/layout/css/custom-rtl.min.css"));
            #endregion
            #region DATA TABLE STYLE
            bundles.Add(new StyleBundle("~/DataTables/css").Include(
                "~/Content/assets/global/plugins/DataTables-1.10.12/media/css/dataTables.bootstrap.min.css"
            ));
            #endregion
            #region TYPEAHEAD STYLE
            bundles.Add(new StyleBundle("~/Typeahead/css").Include(
                "~/Content/assets/global/plugins/typeahead.js-master/media/css/dataTables.bootstrap.min.css"
            ));
            #endregion
            #region DATE ,DATETIME AND DATERANGE PICKERS STYLES
            bundles.Add(new StyleBundle("~/DateTimePickers/css").Include(
                "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css",
                "~/Content/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                "~/Content/assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css",
                "~/Content/assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css",
                "~/Content/assets/global/plugins/clockface/css/clockface.css"
            ));
            #endregion
            #endregion

            #region JS
            #region Metronic CORE PLUGINS

            bundles.Add(new ScriptBundle("~/CorePlugins/js").Include(
                "~/Content/assets/global/plugins/jquery.min.js",
                "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/assets/global/plugins/js.cookie.min.js",
                "~/Content/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/assets/global/plugins/jquery.blockui.min.js",
                //"~/Content/assets/global/plugins/uniform/jquery.uniform.min.js",
                "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                //"~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.js",
                "~/Content/assets/global/plugins/jquery-notific8/jquery.notific8.min.js",
                "~/Content/assets/global/plugins/bootstrap-select/js/bootstrap-select.min.js"
                 ));
            #endregion
            #region THEME GLOBAL SCRIPTS
            bundles.Add(new ScriptBundle("~/GlobalScript/js").Include(
                     "~/Content/assets/global/scripts/app.min.js",
                     "~/Scripts/GeneralScripts.js"));
            #endregion
            #region Metronic THEME LAYOUT SCRIPTS
            #region Metronic THEME LAYOUT SCRIPTS
            bundles.Add(new ScriptBundle("~/ThemeLayoutScripts/js").Include(
                      "~/Content/assets/layouts/layout/scripts/layout.min.js",
                      "~/Content/assets/layouts/layout/scripts/demo.min.js",
                      "~/Content/assets/layouts/global/scripts/quick-sidebar.min.js"));
            #endregion
            #endregion
            #region DATA TABLE SCRIPTS

            bundles.Add(new ScriptBundle("~/DataTables/Js").Include(
                "~/Content/assets/global/plugins/DataTables-1.10.12/media/js/jquery.dataTables.min.js",
                "~/Content/assets/global/plugins/DataTables-1.10.12/media/js/dataTables.bootstrap.min.js"
            // "~/Content/assets/pages/scripts/table-datatables-buttons.min.js"
            //"~/Content/assets/global/plugins/DataTables-1.10.12/media/js/table-datatables-buttons.min.js"
            ));
            #endregion
            #region TYPEAHEAD SCRIPTS
            bundles.Add(new ScriptBundle("~/Typeahead/Js").Include(
                "~/Content/assets/global/plugins/typeahead.js-master/handlebars.min.js",
                "~/Content/assets/global/plugins/typeahead.js-master/dist/bloodhound.min.js",
                "~/Content/assets/global/plugins/typeahead.js-master/dist/typeahead.jquery.min.js"
            ));
            #endregion
            #region DATE ,DATETIME AND DATERANGE PICKERS SCRIPTS 
            bundles.Add(new ScriptBundle("~/DateTimePickers/js").Include(
                "~/Content/assets/global/plugins/moment.min.js",
                "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                "~/Content/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                "~/Content/assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js",
                "~/Content/assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js",
                "~/Content/assets/global/plugins/clockface/js/clockface.js"
            ));
            #endregion
            #region GENERAL SCRIPTS

            #endregion
            #endregion

            BundleTable.EnableOptimizations = false;
        }
    }
}
