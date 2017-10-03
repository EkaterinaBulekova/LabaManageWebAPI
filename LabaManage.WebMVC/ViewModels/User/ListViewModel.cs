using System.Collections.Generic;
using LabaManage.Models.Models;

namespace LabaManage.WebMVC.ViewModels.User
{
    public class ListViewModel
    {
        public IEnumerable<UserModel> Users { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}