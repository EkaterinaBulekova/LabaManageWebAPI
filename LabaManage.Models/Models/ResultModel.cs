using System.Collections.Generic;

namespace LabaManage.Models.Models
{
    public class ResultModel
    {
        public bool IsSucceed { get; set; }

        public int SucceededItems { get; set; }

        public List<string> NonCommited { get; set; }
    }
}