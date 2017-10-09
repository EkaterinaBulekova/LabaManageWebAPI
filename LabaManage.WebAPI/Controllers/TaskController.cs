using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;
using LabaManage.WebAPI.ViewModels.Task;

namespace LabaManageWebAPI.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TaskController : ApiController
    {
        private const int PageSize = 5;
        private const int DefaultPage = 1;
        private const string DefultString = "all";
        private ITaskRepository repository;

        public TaskController(ITaskRepository repo)
        {
            this.repository = repo;
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
            var ratings = new ComentListJson
            {
                Ratings = this.repository.GetRatingsByTask(id, DefaultPage, PageSize).ToList(),
                CurrentUserRating = this.repository.GetRatingByTaskUser(/*User.Identity.Name*/"Ivan", id)
            };
            if (ratings == null)
            {
                return this.NotFound();
            }

            return this.Ok(ratings);
        }

        [Route("~/api/author/{author}/topic/{topic}/level/{level}/tags/{tags}/tasks/page/{page:int}")]
        public IHttpActionResult GetTasksByAuthor(string author, string topic, string level, string tags, int page = DefaultPage)
        {
            int level_temp;
            var tasks = this.GetTasks(
                new FilterModel
                {
                    Author = author != DefultString ? author : string.Empty,
                    Level = level != DefultString && int.TryParse(level, out level_temp) ? level_temp : 0,
                    Topic = topic != DefultString ? topic : string.Empty,
                    Tags = tags != DefultString ? tags : string.Empty
                },
                page);
            if (tasks == null)
            {
                return this.NotFound();
            }

            return this.Ok(tasks);
        }

        [Route("filters")]
        public FilterListsModel GetAllFilters()
        {
            var filterlists = this.repository.GetFilterLists();
            return filterlists;
        }

        private ListJsonModel GetTasks(FilterModel filter, int page = DefaultPage)
        {
            int totalItems = this.repository.GetTasksCount(filter);
            int totalPages = totalItems / PageSize;
            if ((totalItems % PageSize) > 0)
            {
                totalPages++;
            }

            if (totalPages > 3)
            {
                totalPages = 3;
            }

            var result = new ListJsonModel
            {
                TotalPages = totalPages,
                Tasks = this.repository.GetTasksByFilter(filter, DefaultPage, PageSize)
                    .Select(_ => new TaskViewModel
                    {
                        Task = _,
                        PercentFeature = this.repository.GetRatingsByTaskPercents(_.TaskId),
                        RatingsCount = this.repository.GetRatingsByTaskCount(_.TaskId),
                        AvgRating = this.repository.GetAvgRatingByTaskId(_.TaskId)
                    })
            };
            return result;
        }
    }
}
