using System;
using log4net;

namespace LabaManage.WebMVC.Abstract
{
    public interface ILogger
    {
        void Debug(string message);

        void Debug(string message, Exception exception);

        void Debug(Exception exception);

        void Info(string message);
    }
}
