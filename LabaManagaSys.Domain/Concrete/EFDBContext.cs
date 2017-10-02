﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using LabaManage.Data.Abstract;
using LabaManage.Data.EntitiesModel;

namespace LabaManage.Data.Concrete
{
    public class EFDbContext : DbContext, IEFDbContext 
    {
       public EFDbContext() : base("name=EFDbContext")
        {
        }

        public virtual IDbSet<AppUser> AppUsers { get; set; }

        public virtual IDbSet<Role> Roles { get; set; }

        public virtual IDbSet<Lesson> Lessons { get; set; }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Task> Tasks { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Role>()
                .HasMany(e => e.AppUsers)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Lessons)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.AppUsers)
                .WithMany(e => e.Lessons)
                .Map(m => m.ToTable("User_Lessons").MapLeftKey("LessonId").MapRightKey("UserId"));

            modelBuilder.Entity<Task>()
                .Property(t => t.Name)
                .HasColumnAnnotation(
                "Index",
                new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true }));

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Tag>()
            .HasMany(e => e.Tasks)
            .WithMany(e => e.Tags)
            .Map(m => m.ToTable("Tasks_tags").MapLeftKey("TagId").MapRightKey("TaskId"));

            modelBuilder.Entity<Tag>()
            .Property(t => t.Name)
            .HasColumnAnnotation(
            "Index",
            new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true }));
        }
    }
}