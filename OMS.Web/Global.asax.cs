using OMS.Web.Globalization;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static GlobalizationModule GlobalizationModule = new GlobalizationModule();
        public override void Init()
        {
            base.Init();
            GlobalizationModule.Init(this);
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }
}
