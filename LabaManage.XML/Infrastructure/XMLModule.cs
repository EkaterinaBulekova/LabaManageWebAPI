using LabaManage.XML.Abstract;
using LabaManage.XML.Concrete;
using Ninject.Modules;

namespace LabaManage.XML.Infrastructure
{
    public class XMLProcessorModule : NinjectModule
    {
        private string xmlProcessorType;

        public XMLProcessorModule(string xmlProc)
        {
            this.xmlProcessorType = xmlProc;
        }

        public override void Load()
        {
            if (this.xmlProcessorType == "XMLlinq")
            {
                Bind<IxmlProcessor>().To<XMLLinqProcessor>();
            }
            else
            {
                Bind<IxmlProcessor>().To<XPathProcessor>();
            }
        }
    }
}
