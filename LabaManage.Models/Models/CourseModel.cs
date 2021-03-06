﻿using System.ComponentModel.DataAnnotations;
using LabaManage.Data.EntitiesModel;

namespace LabaManage.Models.Models
{
    public class CourseModel
    {
        public CourseModel()
        {
        }

        public CourseModel(Course course)
        {
            this.CourseId = course.CourseId;
            this.Name = course.Name;
        }

        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}