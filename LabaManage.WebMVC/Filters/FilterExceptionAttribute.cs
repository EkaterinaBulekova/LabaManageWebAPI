using System.Web.Mvc;
using LabaManage.WebMVC.Abstract;

namespace LabaManage.WebMVC.Filters
{
    public class FilterExceptionAttribute : HandleErrorAttribute
    {
        private ILogger log;

        public FilterExceptionAttribute(ILogger log)
        {
           this.log = log;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                this.log.Debug(filterContext.Exception);
                base.OnException(filterContext);
            }
        }
    }
}