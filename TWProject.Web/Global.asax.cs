using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Stripe;
using TWProject.Web.Stripe;

namespace TWProject.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var secretKey = ConfigurationManager.AppSettings["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = secretKey;

            var stripeSettings = new StripeSettings
            {
	            SecretKey = secretKey,
	            PublishableKey = ConfigurationManager.AppSettings["Stripe:PublishableKey"]
            };
		}
    }
}
