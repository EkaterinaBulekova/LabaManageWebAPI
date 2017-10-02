using LabaManage.Data.Abstract;
using LabaManage.Data.Concrete;
using Ninject.Modules;

namespace LabaManage.Data.Infrastructure
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEFDbContext>().To<EFDbContext>();
        }
    }
}
