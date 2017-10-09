using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;

namespace LabaManageWebAPI.Controllers
{
    [RoutePrefix("api/tags")]
    public class TagController : ApiController
    {
        private ITaskRepository repository;

        public TagController(ITaskRepository repo)
        {
            this.repository = repo;
        }

        [Route("")]
        public IHttpActionResult GetAllTags()
        {
            var tags = this.repository.GetTags(null).Select(_ => new { value = _.Name, label = _.Name }).ToList();
            return this.Ok(tags);
        }

        [Route("{id:int}")]
        public IHttpActionResult GetTagsByTask(int id)
        {
            var tag = this.repository.GetTags(id);
            if (tag == null)
            {
                return this.NotFound();
            }

            return this.Ok(tag);
        }

        [Route("{name}")]
        public IHttpActionResult GetTagsByName(string name)
        {
            var tags = this.repository.GetTags(name);
            if (tags == null)
            {
                return this.NotFound();
            }

            return this.Ok(tags);
        }

        [Route("{name}/tasks")]
        public IHttpActionResult GetTasksByTags(string name)
        {
            var tasks = this.repository.GetTasksByFilter(new FilterModel { Tags = name }, 1, 100);
            if (tasks == null)
            {
                return this.NotFound();
            }

            return this.Ok(tasks);
        }

        [Route("{id:int}/tasks")]
        public IHttpActionResult GetTaskByTags(int id)
        {
            var tasks = this.repository.GetTasksByTag(id);
            if (tasks == null)
            {
                return this.NotFound();
            }

            return this.Ok(tasks);
        }
    }
}
