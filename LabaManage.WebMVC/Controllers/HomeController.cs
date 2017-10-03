using System.Web.Mvc;
using LabaManage.WebMVC.Abstract;
using LabaManage.WebMVC.ViewModels.Home;

namespace LabaManage.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private ILogger log;

        public HomeController(ILogger logger)
        {
            this.log = logger;
        }
       
        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel { Status = "Вы не авторизованы" };
            if (User.Identity.IsAuthenticated)
            {
                model.Status = "Ваш логин: " + User.Identity.Name + " роль Админ:" + (User.IsInRole("Administrators") ? "да" : "нет");
            }

            return this.View(model);
        }
    }
}