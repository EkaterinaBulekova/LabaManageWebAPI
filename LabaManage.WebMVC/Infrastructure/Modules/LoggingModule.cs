using LabaManage.WebMVC.Abstract;
using LabaManage.WebMVC.Concrete;
using log4net;
using Ninject.Modules;

namespace LabaManage.WebMVC.Infrastructure
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
            Bind<ILogger>().To<Logger>();
        }
    }
}