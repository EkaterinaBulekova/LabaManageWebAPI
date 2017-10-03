using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;

namespace LabaManageWebAPI.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        private ITaskRepository repository;

        public TaskController(ITaskRepository repo)
        {
            this.repository = repo;
        }

        [Route("")]
        public IEnumerable<TaskModel> GetAllTasks()
        {
            var tasks = this.repository.GetTasksByFilter(new FilterModel(), 1, 10).ToList();
            return tasks;
        }

        [Route("{id:int}")]
        public IHttpActionResult GetTaskById(int id)
        {
            var task = this.repository.GetTaskById(id);
            if (task == null)
            {
                return this.NotFound();
            }

            return this.Ok(task);
        }

        [Route("{name}")]
        public IHttpActionResult GetTaskByName(string name)
        {
            var task = this.repository.GetTaskByName(name);
            if (task == null)
            {
                return this.NotFound();
            }

            return this.Ok(task);
        }

        [Route("{id:int}/tags")]
        public IHttpActionResult GetTagsByTask(int id)
        {
            var tags = this.repository.GetTags(id);
            if (tags == null)
            {
                return this.NotFound();
            }

            return this.Ok(tags);
        }

        [Route("{id:int}/ratings")]
        public IHttpActionResult GetRatingsByTask(int id)
        {
            var ratings = this.repository.GetRatingsByTask(id, 1, 100);
            if (ratings == null)
            {
                return this.NotFound();
            }

            return this.Ok(ratings);
        }

        [Route("{id:int}/avgrating")]
        public IHttpActionResult GetAvgRatingByTask(int id)
        {
            var ratings = this.repository.GetAvgRatingByTaskId(id);
            if (ratings < 0)
            {
                return this.NotFound();
            }

            return this.Ok(ratings);
        }

        [Route("{id:int}/user/{name}/ratings")]
        public IHttpActionResult GetRatingsByUserTask(int id, string name)
        {
            var ratings = this.repository.GetRatingByTaskUser(name, id);
            if (ratings == null)
            {
                return this.NotFound();
            }

            return this.Ok(ratings);
        }

        [Route("~/api/topic/{name}/tasks")]
        public IHttpActionResult GetTasksByTopic(string name)
        {
            var tasks = this.repository.GetTasksByFilter(new FilterModel { Topic = name }, 1, 100);
            if (tasks == null)
            {
                return this.NotFound();
            }

            return this.Ok(tasks);
        }

        [Route("~/api/author/{name}/tasks")]
        public IHttpActionResult GetTasksByAuthor(string name)
        {
            var tasks = this.repository.GetTasksByFilter(new FilterModel { Author = name }, 1, 100);
            if (tasks == null)
            {
                return this.NotFound();
            }

            return this.Ok(tasks);
        }

        [Route("~/api/level/{level:int}/tasks")]
        public IHttpActionResult GetTasksByLevel(int level)
        {
            var tasks = this.repository.GetTasksByFilter(new FilterModel { Level = level }, 1, 100);
            if (tasks == null)
            {
                return this.NotFound();
            }

            return this.Ok(tasks);
        }
    }
}
