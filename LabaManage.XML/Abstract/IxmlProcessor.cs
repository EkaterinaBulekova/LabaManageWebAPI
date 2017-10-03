using System.Collections.Generic;
using System.IO;
using LabaManage.Models.Models;

namespace LabaManage.XML.Abstract
{
    public interface IxmlProcessor
    {
        IEnumerable<TaskModel> UploadTasksFromFile(Stream inputStream);

        void DownloadTasksToFile(IEnumerable<TaskModel> models, string fullPath);
    }
}
