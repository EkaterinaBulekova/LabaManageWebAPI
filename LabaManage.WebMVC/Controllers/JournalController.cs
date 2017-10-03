using System.Web.Mvc;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;
using LabaManage.WebMVC.ViewModels.Journal;
using LabaManage.WebMVC.ViewModels.TeachPlan;

namespace LabaManage.WebMVC.Controllers
{
    [Authorize(Roles = "Teachers")]
    public class JournalController : Controller
    {
        private const int PageSize = 10;
        private const int DefaultPage = 1;
        private ILessonRepository lessonRepository;
        private IUserRepository userRepository;

        public JournalController(ILessonRepository repoL, IUserRepository repoU)
        {
            this.lessonRepository = repoL;
            this.userRepository = repoU;
        }

        public ActionResult List()
        {
            ListViewModel model = new ListViewModel { Courses = this.lessonRepository.Courses };
            return this.View(model);
        }

        public ActionResult Edit(int courseId, int page = DefaultPage)
        {
            JournalEditModel model = new JournalEditModel
                {
                    Course = this.lessonRepository.GetCourseById(courseId),
                    Lessons = this.lessonRepository.GetLessonsByCourse(courseId, page, PageSize),
                    Users = this.userRepository.GetUsersInRole("Students"),
                    PagingInfo = 
                    new PagingInfo
                    {
                        CurrentPage = page,
                        TotalItems = this.lessonRepository.LessonsCountByCourse(courseId),
                        ItemsPerPage = PageSize
                    }
            };
            return this.View(model);
        }

        [HttpPost]
        public void AddUserMissLesson()
        {
            var userId = Request.Form.GetValues("userId")[0];
            var lessonId = Request.Form.GetValues("lessonId")[0];
            var userLesson = new UserLesson(lessonId, userId);
            this.lessonRepository.AddUserMissLesson(userLesson);
        }

        [HttpPost]
        public void RemoveUserMissLesson()
        {
            var userId = Request.Form.GetValues("userId")[0];
            var lessonId = Request.Form.GetValues("lessonId")[0];
            var userLesson = new UserLesson(lessonId, userId);
            this.lessonRepository.RemoveUserMissLesson(userLesson);
        }
    }
}