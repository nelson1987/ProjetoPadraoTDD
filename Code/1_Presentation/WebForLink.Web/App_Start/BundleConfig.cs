using System.Web.Optimization;

namespace WebForLink.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            ////SCRIPTS
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.1.12.4/jquery.js",
                        "~/Scripts/AdminLTE.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/date")
                    .Include(
                        "~/Scripts/plugins/input-mask/jquery.inputmask.js",
                        "~/Scripts/plugins/input-mask/jquery.inputmask.extensions.js",
                        "~/Scripts/plugins/input-mask/jquery.inputmask.date.extensions.js",
                        "~/Scripts/plugins/input-mask/jquery.inputmask.phone.extensions.js",
                        "~/Scripts/plugins/daterangepicker/daterangepicker.js",
                        "~/Scripts/plugins/datepicker/bootstrap-datepicker.js",
                        "~/Scripts/plugins/datepicker/locales/bootstrap-datepicker.pt-BR.js"));
            
            //bundles.Add(new ScriptBundle("~/bundles/jquery-1.x").Include(
            //            "~/Scripts/jquery-1.11.3.js",
            //            "~/Scripts/jquery.unobtrusive-ajax.js",
            //            "~/Scripts/AdminLTE.js"));

            //bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
            //        "~/Scripts/jquery.ui.widget.js",
            //        "~/Scripts/jquery.fileupload.js",
            //        "~/Scripts/Views/Shared/FileUpload.js",
            //        "~/Scripts/Views/Shared/DadosFornecedor.js",
            //        "~/Scripts/Views/Shared/Robo.js"
            //    ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate.js",
            //            "~/Scripts/jquery.validate.unobtrusive.js",
            //            "~/Scripts/globalize/globalize.js",
            //            "~/Scripts/jquery.validate.globalize.js",
            //            "~/Scripts/globalize/cultures/globalize.culture.pt-BR.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/fornecedor").Include(
            //    "~/Views/Fornecedores/Scripts/ModificacaoFornecedor.js"
            //));

            //var valBundle = new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate.js",
            //            "~/Scripts/jquery.validate.unobtrusive.js",
            //            "~/Scripts/globalize/globalize.js",
            //            "~/Scripts/jquery.validate.globalize.js",
            //            "~/Scripts/globalize/cultures/globalize.culture.pt-BR.js");
            ////valBundle.Orderer = new AsIsBundleOrderer();
            ////bundles.Add(valBundle);

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/expressive.annotations.validate.js"));

             bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jQuery-File-Upload").Include(
                //<!-- The Templates plugin is included to render the upload/download listings -->
                "~/Scripts/jQuery.FileUpload/vendor/jquery.ui.widget.js",
                "~/Scripts/jQuery.FileUpload/tmpl.min.js",
                //<!-- The Load Image plugin is included for the preview images and image resizing functionality -->
                "~/Scripts/jQuery.FileUpload/load-image.all.min.js",
                //<!-- The Canvas to Blob plugin is included for image resizing functionality -->
                "~/Scripts/jQuery.FileUpload/canvas-to-blob.min.js",
                //"~/Scripts/file-upload/jquery.blueimp-gallery.min.js",
                //<!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
                "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
                //<!-- The basic File Upload plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                //<!-- The File Upload processing plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-process.js",
                //<!-- The File Upload image preview & resize plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-image.js",
                //<!-- The File Upload audio preview plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-audio.js",
                //<!-- The File Upload video preview plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-video.js",
                //<!-- The File Upload validation plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-validate.js",
                //!-- The File Upload user interface plugin -->
                "~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js",
                //Blueimp Gallery 2 
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery.js",
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery-video.js",
                "~/Scripts/blueimp-gallery2/js/blueimp-gallery-indicator.js",
                "~/Scripts/blueimp-gallery2/js/jquery.blueimp-gallery.js"));

            ////ESTILOS
            bundles.Add(new StyleBundle("~/style/date").Include(
                      "~/Content/site/datepicker3.css",
                      "~/Content/site/daterangepicker-bs3.css"));

            bundles.Add(new StyleBundle("~/style/bootstrap").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/fontAwesome.css"));

            bundles.Add(new StyleBundle("~/style/lilas").Include(
                      "~/Content/css/AdminLTE.css"));

            bundles.Add(new StyleBundle("~/style/azul").Include(
                      "~/Content/css/AdminAzul.css"));

            bundles.Add(new StyleBundle("~/style/date").Include(
                      "~/Content/css/datepicker3.css",
                      "~/Content/css/daterangepicker-bs3.css"));

            bundles.Add(new StyleBundle("~/style/institucional").Include(
                   "~/Content/css/institucional.css"));

            bundles.Add(new StyleBundle("~/style/siteInterno").Include(
                "~/Content/bootstrap.css",
                "~/Content/adminLte.css",
                "~/Content/fontAwesome.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/style/site").Include(
                "~/Content/bootstrap.css",
                "~/Content/Institucional.css",
                "~/Content/fontAwesome.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/style/jQuery-File-Upload").Include(
                "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
                "~/Content/jQuery.FileUpload/css/jquery.fileupload-ui.css",
                "~/Content/blueimp-gallery2/css/blueimp-gallery.css",
                "~/Content/blueimp-gallery2/css/blueimp-gallery-video.css",
                "~/Content/blueimp-gallery2/css/blueimp-gallery-indicator.css"));


            bundles.Add(new StyleBundle("~/style/convite").Include(
            "~/Content/fonts/OpenSans.css",
            "~/Content/fonts/Merriweather.css",
            "~/Content/creativePage/magnific-popup.css",
            "~/Content/creativePage/creative.css"));


            //#if DEBUG
            //            BundleTable.EnableOptimizations = false;
            //#else
            BundleTable.EnableOptimizations = true;
            //#endif
        }
    }
}