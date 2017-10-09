using System.Collections.Generic;
using LabaManage.Models.Models;

namespace LabaManage.WebAPI.ViewModels.Task
{
    public class ComentListJson
    {
        public List<RatingModel> Ratings { get; set; }

        public RatingModel CurrentUserRating { get; set; }
    }
}