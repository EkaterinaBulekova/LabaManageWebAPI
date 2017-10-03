using System.Web.Mvc;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;
using LabaManage.WebMVC.Abstract;
using LabaManage.WebMVC.ViewModels.User;

namespace LabaManage.WebMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private const int PageSize = 5;
        private const int DefaultPage = 1;
        private IUserRepository repository;
        private ILogger log;

        public UserController(IUserRepository repo, ILogger log)
        {
            this.log = log;
            this.repository = repo;
        }

        public ViewResult List(int page = DefaultPage)
        {
            ListViewModel model = new ListViewModel
            {
                Users = this.repository.GetUserList(page, PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = this.repository.GetUsersCount(),
                    ItemsPerPage = PageSize
                }
            };
            return this.View(model);
        }

        [Authorize(Roles = "Administrators")]
        public ViewResult Edit(int userId)
        {
            EditViewModel model = new EditViewModel
            {
                User = this.repository.GetUserById(userId),
                Roles = this.repository.Roles
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var message = "User " + user.Name + ((user.UserId == 0) ? " created" : " edited");
                this.repository.UserUpdate(user);
                this.TempData["message"] = string.Format("Изменения в пользователе \"{0}\" были сохранены", user.Name);
                this.log.Info(message);
                return this.RedirectToAction("List");
            }
            else
            {
                return this.View(new EditViewModel
                {
                    User = user,
                    Roles = this.repository.Roles
                });
            }
        }

        [Authorize(Roles = "Administrators")]
        public ViewResult Create()
        {
            var model = new EditViewModel
            {
                User = new UserModel { RoleId = this.repository.GetFirstRole().RoleId },
                Roles = this.repository.Roles
            };

            return this.View("Edit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int userId)
        {
            UserModel deletedUser = this.repository.UserDelete(userId);
            if (deletedUser != null)
            {
                this.TempData["message"] = string.Format("Пользователь \"{0}\" успешно удален.", deletedUser.Name);
            }

            return this.RedirectToAction("List");
        }
    }
}