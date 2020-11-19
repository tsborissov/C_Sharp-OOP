using SOLID.Appenders;
using SOLID.Enums;
using System;

namespace SOLID.Loggers
{
    public class Logger : ILogger
    {
        // TODO: think about the ctor

        private IAppender[] appenders;

        public Logger(params IAppender[] appenders)
        {
            this.Appenders = appenders;
        }
        
        public IAppender[] Appenders 
        {
            get
            {
                return this.appenders;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Appenders), "Value cannot be null.");
                }

                this.appenders = value;
            }
        }

        public void Critical(string dateTime, string message)
        {
            Append(dateTime, ReportLevel.Critical, message);
        }

        public void Error(string dateTime, string message)
        {
            Append(dateTime, ReportLevel.Error, message);
        }

        public void Fatal(string dateTime, string message)
        {
            Append(dateTime, ReportLevel.Fatal, message);
        }

        public void Info(string dateTime, string message)
        {
            Append(dateTime, ReportLevel.Info, message);
        }

        public void Warning(string dateTime, string message)
        {
            Append(dateTime, ReportLevel.Warning, message);
        }

        private void Append(string dateTime, ReportLevel error, string message)
        {
            foreach (var appender in this.Appenders)
            {
                appender.Append(dateTime, error, message);
            }
        }
    }
}
