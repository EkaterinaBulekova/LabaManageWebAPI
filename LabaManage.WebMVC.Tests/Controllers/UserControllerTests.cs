using System.Collections.Generic;
using System.Web.Mvc;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;
using LabaManage.WebMVC.Abstract;
using LabaManage.WebMVC.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabaManage.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            Mock<ILogger> mockLogger = new Mock<ILogger>();

            // Организация - создание контроллера
            UserController controller = new UserController(mock.Object, mockLogger.Object);

            // Организация - создание объекта UserModel
            UserModel user = new UserModel { Name = "Test" };

            // Действие - попытка сохранения пользователя
            ActionResult result = controller.Edit(user);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.UserUpdate(user));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            Mock<ILogger> mockLogger = new Mock<ILogger>();

            // Организация - создание контроллера
            UserController controller = new UserController(mock.Object, mockLogger.Object);

            // Организация - создание объекта UserModel
            UserModel user = new UserModel { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения пользователя
            ActionResult result = controller.Edit(user);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.UserUpdate(It.IsAny<UserModel>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Users()
        {
            // Организация - создание объекта UserModel
            UserModel user = new UserModel { UserId = 2, Name = "User2" };
            Mock<ILogger> mockLogger = new Mock<ILogger>();

            // Организация - создание имитированного хранилища данных
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            mock.Setup(m => m.Users).Returns(new List<UserModel>
            {
                new UserModel { UserId = 1, Name = "User1" },
                new UserModel { UserId = 2, Name = "User2" },
                new UserModel { UserId = 3, Name = "User3" },
                new UserModel { UserId = 4, Name = "User4" },
                new UserModel { UserId = 5, Name = "User5" },
                new UserModel { UserId = 6, Name = "User6" },
                new UserModel { UserId = 7, Name = "User7" },
                new UserModel { UserId = 8, Name = "User8" },
                new UserModel { UserId = 9, Name = "User9" },
            });

            // Организация - создание контроллера
            UserController controller = new UserController(mock.Object, mockLogger.Object);

            controller.Delete(user.UserId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта UserModel
            mock.Verify(m => m.UserDelete(user.UserId));
        }
    }
}
