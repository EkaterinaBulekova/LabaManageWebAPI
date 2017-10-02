using LabaManage.Models.Models;
using System.Collections.Generic;

namespace LabaManage.BLL.Abstract
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> Tasks { get; }

        int GetTasksCount(FilterModel filter);

        IEnumerable<TaskModel> GetTasksByFilter(FilterModel filterModel, int page, int pageSize);

        IEnumerable<TaskModel> GetTasksByTag(int tagId);

        TaskModel TaskDelete(int taskId);

        TaskModel GetTaskById(int taskId);

        TaskModel GetTaskByName(string name);

        void TaskUpdate(TaskModel task);

        ResultModel TaskAddAll(IEnumerable<TaskModel> tasks);

        double GetAvgRatingByTaskId(int taskId);

        RatingModel GetRatingByTaskUser(string userName, int taskId);

        IEnumerable<RatingModel> GetRatingsByTask(int taskId, int page, int pageSize);

        void RatingDelete(int ratingId);

        void RatingUpdate(RatingModel rating);

        int[] GetRatingsByTaskPercents(int taskId);

        int GetRatingsByTaskCount(int taskId);

        IEnumerable<TagModel> GetTags(string text);

        IEnumerable<TagModel> GetTags(int taskId);
    }
}
