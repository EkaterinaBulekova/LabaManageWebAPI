using LabaManage.BLL.Abstract;
using LabaManage.BLL.Concrete;
using Ninject.Modules;

namespace LabaManage.BLL.Infrastructure
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ITaskRepository>().To<TaskRepository>();
            Bind<ILessonRepository>().To<LessonRepository>();
        }
    }
}
