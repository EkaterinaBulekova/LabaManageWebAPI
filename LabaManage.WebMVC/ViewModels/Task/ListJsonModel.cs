using System.Collections.Generic;

namespace LabaManage.WebMVC.ViewModels.Task
{
    public class ListJsonModel
    {
        public int TotalPages { get; set; }

        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}