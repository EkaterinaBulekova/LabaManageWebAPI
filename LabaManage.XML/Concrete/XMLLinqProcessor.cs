using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using LabaManage.Models.Models;
using LabaManage.XML.Abstract;

namespace LabaManage.XML.Concrete
{
    public class XMLLinqProcessor : IxmlProcessor
    {
        public void DownloadTasksToFile(IEnumerable<TaskModel> tasks, string fullPath)
        {
            XDocument xdocument = new XDocument(new XElement(
                "Tasks", 
                tasks.Select(_ => new XElement(
                    "Task",
                    new XElement("Author", _.Author),
                    new XElement("Level", _.Level.ToString()),
                    new XElement("Topic", _.Topic),
                    new XElement("Name", _.Name),
                    new XElement("Text", _.Text)))));
            xdocument.Save(fullPath);
        }

        public IEnumerable<TaskModel> UploadTasksFromFile(Stream inputStream)
        {
            var xdocument = XDocument.Load(inputStream);
            var tasks = new List<TaskModel>();
            foreach (var node in xdocument.Root.Elements())
            {
                tasks.Add(new TaskModel
                {
                    Author = node.Element("Author").Value,
                    Level = int.Parse(node.Element("Level").Value),
                    Topic = node.Element("Topic").Value,
                    Name = node.Element("Name").Value,
                    Text = node.Element("Text").Value
                });
            }

            inputStream.Dispose();
            return tasks;
        }
    }
}