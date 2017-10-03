﻿using System.Linq;
using System.Web.Mvc;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;
using LabaManage.WebMVC.ViewModels.Task;

namespace LabaManage.WebMVC.Controllers
{
    [Authorize(Roles = "Teachers")]
    public class TaskManageController : Controller
    {
        private const int PageSize = 5;
        private const int DefaultPage = 1;
        private ITaskRepository repository;

        public TaskManageController(ITaskRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List(FilterModel filter, int page = DefaultPage)
        {
            ListViewModel model = 
                new ListViewModel
                {
                    Tasks = this.repository.GetTasksByFilter(filter, page, PageSize)
                    .Select(_ => new TaskViewModel
                    {
                        Task = _,
                        PercentFeature = this.repository.GetRatingsByTaskPercents(_.TaskId),
                        RatingsCount = this.repository.GetRatingsByTaskCount(_.TaskId),
                        AvgRating = this.repository.GetAvgRatingByTaskId(_.TaskId)
                    }),
                    Filter = filter,
                    Lists = this.repository.GetFilterLists(),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = this.repository.GetTasksCount(filter)
                    }
                };
            return this.View(model);
        }

        public ActionResult GetTaskComentsJson(int taskId)
        {
            var result = Json(
                new ComentListJson
                {
                    Ratings = this.repository.GetRatingsByTask(taskId, DefaultPage, PageSize).ToList(),
                    CurrentUserRating = this.repository.GetRatingByTaskUser(User.Identity.Name, taskId)
                },
                JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public void DeleteComment(int commentId)
        {
            this.repository.RatingDelete(commentId);
        }

        [HttpPost]
        public void SaveComment(RatingModel rating)
        {
            int i = 1;
            if (ModelState.IsValid)
            {
                i = i + 1;
                this.repository.RatingUpdate(rating);
            }
        }

        public ActionResult GetTasksDataJson(FilterModel filter, string author, string topic, string level, string tags)
        {
            filter.Level = (level == string.Empty) ? 0 : (level != null) ? int.Parse(level) : filter.Level;
            filter.Author = (author == string.Empty) ? string.Empty : author ?? filter.Author;
            filter.Topic = (topic == string.Empty) ? string.Empty : topic ?? filter.Topic;
            filter.Tags = (tags == string.Empty) ? string.Empty : tags ?? filter.Tags;
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

            var result = Json(
                new ListJsonModel
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
                },
                JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult GetTagsDataJson(string text)
        {
            var result = Json(
                this.repository.GetTags(text).Select(_ => new { value = _.Name, label = _.Name }).ToList(),
                JsonRequestBehavior.AllowGet);
            return result;
        }

        public ViewResult Edit(int taskId)
        {
            EditViewModel model = new EditViewModel
            {
                Task = this.repository.GetTaskById(taskId)
            };
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                if (this.repository.GetTaskByName(task.Name) != null && task.TaskId == 0)
                {
                    this.TempData["message"] = string.Format("Нельзя сохранить, задание с таким наименованием уже есть.");
                }
                else
                {
                    this.repository.TaskUpdate(task);
                    this.TempData["message"] = string.Format("Изменения в задании были сохранены.");
                    return this.RedirectToAction("List");
                }
            }

            return this.View(new EditViewModel
            {
                Task = task
            });
        }

        public ViewResult Create()
        {
            var model = new EditViewModel
            {
                Task = new TaskModel()
            };
            return this.View("Edit", model);
        }

        [HttpPost]
        public ActionResult Delete(int taskId)
        {
            TaskModel deletedTask = this.repository.TaskDelete(taskId);
            if (deletedTask != null)
            {
                this.TempData["message"] = string.Format("Задание успешно удалено.");
            }

            return this.RedirectToAction("List");
        }
    }
}
