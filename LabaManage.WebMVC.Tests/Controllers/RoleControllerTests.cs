using System.Collections.Generic;
using System.Web.Mvc;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;
using LabaManage.WebMVC.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabaManage.Tests
{
    [TestClass]
    public class RoleControllerTests
    {
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUserRepository> mock = new Mock<IUserRepository>();

            // Организация - создание контроллера
            RoleController controller = new RoleController(mock.Object);

            // Организация - создание объекта RoleModel
            RoleModel role = new RoleModel { Name = "Test" };

            // Действие - попытка сохранения пользователя
            ActionResult result = controller.Edit(role);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.RoleUpdate(role));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUserRepository> mock = new Mock<IUserRepository>();

            // Организация - создание контроллера
            RoleController controller = new RoleController(mock.Object);

            // Организация - создание объекта RoleModel
            RoleModel role = new RoleModel { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения пользователя
            ActionResult result = controller.Edit(role);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.RoleUpdate(It.IsAny<RoleModel>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Roles()
        {
            // Организация - создание объекта RoleModel
            RoleModel role = new RoleModel { RoleId = 2, Name = "Role2" };

            // Организация - создание имитированного хранилища данных
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Roles).Returns(new List<RoleModel>
            {
                new RoleModel { RoleId = 1, Name = "Role1" },
                new RoleModel { RoleId = 2, Name = "Role2" },
                new RoleModel { RoleId = 3, Name = "Role3" },
                new RoleModel { RoleId = 4, Name = "Role4" },
            });

            // Организация - создание контроллера
            RoleController controller = new RoleController(mock.Object);

            controller.Delete(role.RoleId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта RoleModel
            mock.Verify(m => m.RoleDelete(role.RoleId));
        }
    }
}
