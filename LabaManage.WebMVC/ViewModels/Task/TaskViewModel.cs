using LabaManage.Models.Models;

namespace LabaManage.WebMVC.ViewModels.Task
{
    public class TaskViewModel
    {
        public TaskModel Task { get; set; }
        
        public int[] PercentFeature { get; set; }

        public int RatingsCount { get; set; }

        public double AvgRating { get; set; }
    }
}