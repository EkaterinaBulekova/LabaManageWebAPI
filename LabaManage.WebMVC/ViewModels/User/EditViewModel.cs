using System.Collections.Generic;
using LabaManage.Models.Models;

namespace LabaManage.WebMVC.ViewModels.User
{
    public class EditViewModel
    {
        public UserModel User { get; set; }

        public IEnumerable<RoleModel> Roles { get; set; }
    }
}