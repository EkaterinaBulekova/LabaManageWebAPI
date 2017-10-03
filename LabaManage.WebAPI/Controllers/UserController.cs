using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;

namespace LabaManageWebAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private IUserRepository repository;

        public UserController(IUserRepository repo)
        {
            this.repository = repo;
        }

        [Route("")]
        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = this.repository.GetUserList(1, 10).ToList();
            return users;
        }

        [Route("{id:int}")]
        public IHttpActionResult GetUser(int id)
        {
            var user = this.repository.GetUserById(id);
            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(user);
        }

        [Route("{name}")]
        public IHttpActionResult GetUser(string name)
        {
            var user = this.repository.GetUserByName(name);
            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(user);
        }

        [Route("count")]
        public IHttpActionResult GetUserCount()
        {
            var count = this.repository.GetUsersCount();
            if (count == 0)
            {
                return this.NotFound();
            }

            return this.Ok(count);
        }

        [Route("{name}/roles")]
        public IHttpActionResult GetRolesForUser(string name)
        {
            var roles = this.repository.GetRolesForUser(name);
            if (roles == null)
            {
                return this.NotFound();
            }

            return this.Ok(roles);
        }
    }
}
