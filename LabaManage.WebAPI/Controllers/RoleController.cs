using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;

namespace LabaManageWebAPI.Controllers
{
    [RoutePrefix("api/role")]
    public class RoleController : ApiController
    {
        private IUserRepository repository;

        public RoleController(IUserRepository repo)
        {
            this.repository = repo;
        }

        [Route("")]
        public IEnumerable<RoleModel> GetAllRoles()
        {
            var roles = this.repository.Roles.ToList();
            return roles;
        }

        [Route("{id:int}")]
        public IHttpActionResult GetRole(int id)
        {
            var role = this.repository.GetRoleById(id);
            if (role == null)
            {
                return this.NotFound();
            }

            return this.Ok(role);
        }

        [Route("{name}")]
        public IHttpActionResult GetRole(string name)
        {
            var role = this.repository.GetRoleByName(name);
            if (role == null)
            {
                return this.NotFound();
            }

            return this.Ok(role);
        }

        [Route("{name}/users")]
        public IHttpActionResult GetUsersInRole(string name)
        {
            var users = this.repository.GetUsersInRole(name);
            if (users == null)
            {
                return this.NotFound();
            }

            return this.Ok(users);
        }
    }
}