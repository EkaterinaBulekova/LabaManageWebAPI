using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LabaManage.Models.Models;
using LabaManage.WebMVC.App_Start;
using LabaManage.WebMVC.Infrastructure.Binders;

namespace LabaManage.WebMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(FilterModel), new FilterModelBinder());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
