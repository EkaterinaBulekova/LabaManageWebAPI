using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LabaManage.BLL.Abstract;
using LabaManage.Models.Models;

namespace LabaManageWebAPI.Controllers
{
    [RoutePrefix("api/courses")]
    public class LessonController : ApiController
    {
        private ILessonRepository repository;

        public LessonController(ILessonRepository repo)
        {
            this.repository = repo;
        }

        [Route("")]
        public IEnumerable<CourseModel> GetAllCourses()
        {
            var courses = this.repository.Courses.ToList();
            return courses;
        }

        [Route("{id:int}")]
        public IHttpActionResult GetCourse(int id)
        {
            var course = this.repository.GetCourseById(id);
            if (course == null)
            {
                return this.NotFound();
            }

            return this.Ok(course);
        }

        [Route("{id:int}/count")]
        public IHttpActionResult GetLessonCountByCourse(int id)
        {
            var count = this.repository.LessonsCountByCourse(id);
            if (count == 0)
            {
                return this.NotFound();
            }

            return this.Ok(count);
        }

        [Route("{id:int}/lessons")]
        public IHttpActionResult GetLessonByCourse(int id)
        {
            var roles = this.repository.GetLessonsByCourse(id, 1, 100);
            if (roles == null)
            {
                return this.NotFound();
            }

            return this.Ok(roles);
        }
    }
}
