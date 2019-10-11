using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TechSupportWeb.Services.Interfaces;

namespace TechSupportWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var scheduler = DependencyResolver.Current.GetService<IScheduler>();
            scheduler.Start();
        }

        protected void Application_End()
        {
            var scheduler = DependencyResolver.Current.GetService<IScheduler>();
            scheduler.Stop();
        }
    }
}
