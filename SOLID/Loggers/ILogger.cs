using SOLID.Appenders;

namespace SOLID.Loggers
{
    public interface ILogger
    {
        IAppender[] Appenders { get; }

        public void Info(string dateTime, string message);

        public void Warning(string dateTime, string message);

        public void Error(string dateTime, string message);

        public void Critical(string dateTime, string message);

        public void Fatal(string dateTime, string message);

    }
}
