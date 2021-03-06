﻿using System.Web.Mvc;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;
using LabaManage.WebMVC.ViewModels.Role;

namespace LabaManage.WebMVC.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class RoleController : Controller
    {
        private IUserRepository repository;

        public RoleController(IUserRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List()
        {
            ListViewModel roles = new ListViewModel { Roles = this.repository.Roles };
            return this.View(roles);
        }

        public ViewResult Edit(int roleId)
        {
            EditViewModel viewRole = new EditViewModel { Role = this.repository.GetRoleById(roleId) };
            return this.View(viewRole);
        }

        [HttpPost]
        public ActionResult Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                this.repository.RoleUpdate(role);
                this.TempData["message"] = string.Format("Изменения в роли \"{0}\" были сохранены", role.Name);
                return this.RedirectToAction("List");
            }
            else
            {
                return this.View(role);
            }
        }

        public ViewResult Create()
        {
            return this.View("Edit", new EditViewModel { Role = new RoleModel() });
        }

        [HttpPost]
        public ActionResult Delete(int roleId)
        {
            if (!this.repository.AreUsersInRole(roleId))
            {
                RoleModel deletedRole = this.repository.RoleDelete(roleId);
                if (deletedRole != null)
                {
                    this.TempData["message"] = string.Format("Роль \"{0}\" успешно удалена.", deletedRole.Name);
                }
            }
            else
            {
                this.TempData["message"] = string.Format("Роль \"{0}\" нельзя удалить пока есть пользователи в этой роли.", this.repository.GetRoleById(roleId).Name);
            }

            return this.RedirectToAction("List");
        }
    }
}