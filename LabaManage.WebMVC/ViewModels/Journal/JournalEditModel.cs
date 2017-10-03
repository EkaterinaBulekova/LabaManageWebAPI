using System.Collections.Generic;
using LabaManage.Models.Models;

namespace LabaManage.WebMVC.ViewModels.Journal
{
    public class JournalEditModel
    {
        public CourseModel Course { get; set; }

        public IEnumerable<UserModel> Users { get; set; }

        public IEnumerable<LessonModel> Lessons { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}