using System.Collections.Generic;
using LabaManage.Models.Models;

namespace LabaManage.WebMVC.ViewModels.Role
{
    public class ListViewModel
    {
        public IEnumerable<RoleModel> Roles { get; set; }
    }
}