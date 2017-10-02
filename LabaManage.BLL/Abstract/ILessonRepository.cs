using System;
using System.Collections.Generic;
using LabaManage.Models.Models;


namespace LabaManage.BLL.Abstract
{
    public interface ILessonRepository
    {
        IEnumerable<CourseModel> Courses { get; }

        IEnumerable<LessonModel> Lessons { get; }

        int LessonsCountByCourse(int courseId);

        IEnumerable<LessonModel> GetLessonsByCourse(int courseId, int page, int pageSize);

        CourseModel CourseDelete(int courseId);

        CourseModel GetCourseById(int courseId);

        IEnumerable<DateTime> GetDatesByCourse(int courseId);

        void CourseUpdate(CourseModel course, IEnumerable<DateTime> dates);

        void AddUserMissLesson(UserLesson userLesson);

        void RemoveUserMissLesson(UserLesson userLesson);
    }
}
