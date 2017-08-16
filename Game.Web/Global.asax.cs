using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Game.Web.Models;

namespace Game.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GameActorSystem.Create();
        }

        private void Application_End()
        {
            GameActorSystem.Shutdown();
        }
    }
}
