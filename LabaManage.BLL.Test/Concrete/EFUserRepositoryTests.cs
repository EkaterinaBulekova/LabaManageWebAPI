using System.Collections.Generic;
using System.Linq;
using LabaManage.BLL.Abstract;
using LabaManage.BLL.Concrete;
using LabaManage.BLL.Tests.Fakes;
using LabaManage.Data.Abstract;
using LabaManage.Data.EntitiesModel;
using LabaManage.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabaManage.Tests.Concrete
{
    [TestClass]
    public class EFUserRepositoryTests
    {
        [TestMethod]
        public void GetUserListTest()
        {
            // Arrange
            var mock = new Mock<IEFDbContext>();
            mock.Setup(x => x.AppUsers)
                .Returns(new FakeDbSet<AppUser>
                {
                    new AppUser() { UserId = 1, Name = "User1" },
                    new AppUser() { UserId = 2, Name = "User2" },
                    new AppUser() { UserId = 3, Name = "User3" },
                    new AppUser() { UserId = 4, Name = "User4" },
                    new AppUser() { UserId = 5, Name = "User5" },
                    new AppUser() { UserId = 6, Name = "User6" },
                    new AppUser() { UserId = 7, Name = "User7" },
                    new AppUser() { UserId = 8, Name = "User8" },
                    new AppUser() { UserId = 9, Name = "User9" }
                });

            // mock.Setup(m => m.AppUsers).Returns();
            IUserRepository repository = new UserRepository(mock.Object);
            
            // Действие 
            IEnumerable<UserModel> result = repository.GetUserList(2, 5);

            // Утверждение 
            List<UserModel> users = result.ToList();
            Assert.IsTrue(users.Count == 4);
            Assert.AreEqual(users[0].Name, "User6");
            Assert.AreEqual(users[1].Name, "User7");
            Assert.AreEqual(users[2].Name, "User8");
            Assert.AreEqual(users[3].Name, "User9");
        }

        [TestMethod]
        public void GetUsersCount()
        {
            // Arrange
            var mock = new Mock<IEFDbContext>();
            mock.Setup(x => x.AppUsers)
                .Returns(new FakeDbSet<AppUser>
                {
                    new AppUser() { UserId = 1, Name = "User1" },
                    new AppUser() { UserId = 2, Name = "User2" },
                    new AppUser() { UserId = 3, Name = "User3" },
                    new AppUser() { UserId = 4, Name = "User4" },
                    new AppUser() { UserId = 5, Name = "User5" },
                    new AppUser() { UserId = 6, Name = "User6" },
                    new AppUser() { UserId = 7, Name = "User7" },
                    new AppUser() { UserId = 8, Name = "User8" },
                    new AppUser() { UserId = 9, Name = "User9" }
                });

            // mock.Setup(m => m.AppUsers).Returns();
            IUserRepository repository = new UserRepository(mock.Object);

            // Действие 
            var result = repository.GetUsersCount();

            // Утверждение 
            Assert.IsTrue(result == 9);
        }

        [TestMethod]
        public void GetUserById()
        {
            // Arrange
            var mock = new Mock<IEFDbContext>();
            mock.Setup(x => x.AppUsers)
                .Returns(new FakeDbSet<AppUser>
                {
                    new AppUser() { UserId = 1, Name = "User1" },
                    new AppUser() { UserId = 2, Name = "User2" },
                    new AppUser() { UserId = 3, Name = "User3" },
                    new AppUser() { UserId = 4, Name = "User4" },
                    new AppUser() { UserId = 5, Name = "User5" },
                    new AppUser() { UserId = 6, Name = "User6" },
                    new AppUser() { UserId = 7, Name = "User7" },
                    new AppUser() { UserId = 8, Name = "User8" },
                    new AppUser() { UserId = 9, Name = "User9" }
                });

            // mock.Setup(m => m.AppUsers).Returns();
            IUserRepository repository = new UserRepository(mock.Object);

            // Действие 
            UserModel result = repository.GetUserById(2);

            // Утверждение 
            Assert.AreEqual(result.Name, "User2");
        }

        [TestMethod]
        public void GetUserByName()
        {
            // Arrange
            var mock = new Mock<IEFDbContext>();
            mock.Setup(x => x.AppUsers)
                .Returns(new FakeDbSet<AppUser>
                {
                    new AppUser() { UserId = 1, Name = "User1" },
                    new AppUser() { UserId = 2, Name = "User2" },
                    new AppUser() { UserId = 3, Name = "User3" },
                    new AppUser() { UserId = 4, Name = "User4" },
                    new AppUser() { UserId = 5, Name = "User5" },
                    new AppUser() { UserId = 6, Name = "User6" },
                    new AppUser() { UserId = 7, Name = "User7" },
                    new AppUser() { UserId = 8, Name = "User8" },
                    new AppUser() { UserId = 9, Name = "User9" }
                });

            // mock.Setup(m => m.AppUsers).Returns();
            IUserRepository repository = new UserRepository(mock.Object);

            // Действие 
            UserModel result = repository.GetUserByName("User5");

            // Утверждение 
            Assert.IsTrue(result.UserId == 5);
        }

        [TestMethod]
        public void GetRoleById()
        {
            // Arrange
            var mock = new Mock<IEFDbContext>();
            mock.Setup(x => x.Roles)
                .Returns(new FakeDbSet<Role>
                {
                    new Role() { RoleId = 1, Name = "Admin" },
                    new Role() { RoleId = 2, Name = "User" },
                    new Role() { RoleId = 3, Name = "Moder" },
                });

            // mock.Setup(m => m.AppUsers).Returns();
            IUserRepository repository = new UserRepository(mock.Object);

            // Действие 
            RoleModel result = repository.GetRoleById(2);

            // Утверждение 
            Assert.AreEqual(result.Name, "User");
        }

        [TestMethod]
        public void GetRoleByName()
        {
            // Arrange
            var mock = new Mock<IEFDbContext>();
            mock.Setup(x => x.Roles)
                .Returns(new FakeDbSet<Role>
                {
                    new Role() { RoleId = 1, Name = "Admin" },
                    new Role() { RoleId = 2, Name = "User" },
                    new Role() { RoleId = 3, Name = "Moder" },
                });

            // mock.Setup(m => m.AppUsers).Returns();
            IUserRepository repository = new UserRepository(mock.Object);

            // Действие 
            RoleModel result = repository.GetRoleByName("Admin");

            // Утверждение 
            Assert.IsTrue(result.RoleId == 1);
        }
    }
}
