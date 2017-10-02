using System.Data.Entity;
using LabaManage.Data.EntitiesModel;

namespace LabaManage.Data.Abstract
{
    public interface IEFDbContext
    {
        IDbSet<AppUser> AppUsers { get; set; }

        IDbSet<Role> Roles { get; set; }

        IDbSet<Lesson> Lessons { get; set; }

        IDbSet<Course> Courses { get; set; }

        IDbSet<Task> Tasks { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Rating> Ratings { get; set; }

        int SaveChanges();
    }
}
