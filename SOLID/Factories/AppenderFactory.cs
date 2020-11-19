using SOLID.Appenders;
using SOLID.Enums;
using SOLID.Layouts;
using SOLID.Loggers;
using System;

namespace SOLID.Factories
{
    public class AppenderFactory
    {
        public static IAppender CreateAppender(string type, ILayout layout, ReportLevel reportLevel)
        {

            // TODO : string type, ILayout layout, ReportLevel reportLevel, ILogFile log = null
            switch (type)
            {
                case "ConsoleAppender":
                    return new ConsoleAppender(layout) { ReportLevel = reportLevel};

                case "FileAppender":
                    return new FileAppender(layout, new LogFile()) { ReportLevel = reportLevel};

                default:
                    throw new ArgumentException("Invalid Appender Type!");
            }
        }
    }
}
