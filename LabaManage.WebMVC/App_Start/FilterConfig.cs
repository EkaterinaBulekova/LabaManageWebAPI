using System.Web.Mvc;
using LabaManage.WebMVC.Abstract;
using LabaManage.WebMVC.Filters;

namespace LabaManage.WebMVC.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new FilterExceptionAttribute(DependencyResolver.Current.GetService<ILogger>()));
        }
    }
}