using System.Web;
using System.Web.Optimization;

namespace TWProject.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
               //Style Bundles
			bundles.Add(new StyleBundle("~/bundles/icomoon/css").Include("~/Vendors/fonts/icomoon/style.css", new CssRewriteUrlTransform()));

               bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Vendors/css/bootstrap.min.css", new CssRewriteUrlTransform()));
               bundles.Add(new StyleBundle("~/bundles/bootstrap-datepicker/css").Include("~/Vendors/css/bootstrap-datepicker.css", new CssRewriteUrlTransform()));
               bundles.Add(new StyleBundle("~/bundles/jquery/css").Include("~/Vendors/css/jquery.fancybox.min.css", new CssRewriteUrlTransform()));
               bundles.Add(new StyleBundle("~/bundles/owl-carousel/css").Include("~/Vendors/css/owl.carousel.min.css", new CssRewriteUrlTransform()));
               bundles.Add(new StyleBundle("~/bundles/owl-default/css").Include("~/Vendors/css/owl.theme.default.min.css\"", new CssRewriteUrlTransform()));
               bundles.Add(new StyleBundle("~/bundles/flaticon/css").Include("~/Vendors/fonts/flaticon/font/flaticon.css", new CssRewriteUrlTransform()));
               bundles.Add(new StyleBundle("~/bundles/aos/css").Include("~/Vendors/css/aos.css", new CssRewriteUrlTransform()));

               //Main CSS
               bundles.Add(new StyleBundle("~/bundles/style/css").Include("~/Vendors/css/style.css", new CssRewriteUrlTransform()));

               //Script Bundles
               bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include("~/Vendors/js/jquery-3.3.1.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/popper/js").Include("~/Vendors/js/popper.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include("~/Vendors/js/bootstrap.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/owl-carousel/js").Include("~/Vendors/js/owl.carousel.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/jquery-sticky/js").Include("~/Vendors/js/jquery.sticky.js"));
               bundles.Add(new ScriptBundle("~/bundles/jquery-waypoints/js").Include("~/Vendors/js/jquery.waypoints.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/jquery-animate/js").Include("~/Vendors/js/jquery.animateNumber.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/jquery-fancybox/js").Include("~/Vendors/js/jquery.fancybox.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/jquery-easing/js").Include("~/Vendors/js/jquery.easing.1.3.js"));
               bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker/js").Include("~/Vendors/js/bootstrap-datepicker.min.js"));
               bundles.Add(new ScriptBundle("~/bundles/aos/js").Include("~/Vendors/js/aos.js"));

               bundles.Add(new ScriptBundle("~/bundles/main/js").Include("~/Vendors/js/main.js"));
		}
	}
}
