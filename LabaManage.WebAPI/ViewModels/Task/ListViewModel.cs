using System.Collections.Generic;
using System.Linq;
using LabaManage.Models.Models;

namespace LabaManage.WebAPI.ViewModels.Task
{
    public class ListViewModel
    {
        public IEnumerable<TaskViewModel> Tasks { get; set; }

        public FilterListsModel Lists { get; set; }

        public FilterModel Filter { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public void SetTotalItems()
        {
            this.PagingInfo.TotalItems = this.Tasks.Count();
        }
    }
}