using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace AdministrationPortal
{
    public class MvcApplication : HttpApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var raisedException = Server.GetLastError();

            Logger.Error(raisedException, $"Uncaught exception: {raisedException}");
        }
    }
}
