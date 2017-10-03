using System.Collections.Generic;
using LabaManage.Models.Models;

namespace LabaManage.WebMVC.ViewModels.TeachPlan
{
    public class ListViewModel
    {
        public IEnumerable<CourseModel> Courses { get; set; }
    }
}