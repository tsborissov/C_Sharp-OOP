using SOLID.Appenders;
using SOLID.Enums;
using SOLID.Factories;
using SOLID.Layouts;
using SOLID.Loggers;
using System;
using System.Collections.Generic;

namespace SOLID.Core
{
    public class CommandInterpreter
    {
        private readonly List<IAppender> appenders;
        private ILogger logger;

        //ILogger logger = new Logger(appenders.ToArray());

        public CommandInterpreter()
        {
            this.appenders = new List<IAppender>();
        }


        public void Read(string[] args)
        {
            string command = args[0];

            if (command.Contains("Appender"))
            {
                CreateAppender(args);
            }
            else
            {
                logger = new Logger(this.appenders.ToArray());
                AppendMessage(args);
            }

        }

        public void PrintInfo()
        {
            Console.WriteLine("Logger info");

            foreach (var appender in appenders)
            {
                Console.WriteLine(appender);
            }
        }

        private void CreateAppender(string[] inputInfo)
        {
            string appenderType = inputInfo[0];
            string layoutType = inputInfo[1];
            ReportLevel reportLevel = ReportLevel.Info;

            if (inputInfo.Length > 2)
            {
                reportLevel = Enum.Parse<ReportLevel>(inputInfo[2], true);
            }

            ILayout layout = LayoutFactory.CreateLayout(layoutType);
            IAppender appender = AppenderFactory.CreateAppender(appenderType, layout, reportLevel);

            appenders.Add(appender);
        }

        private void AppendMessage(string[] inputInfo)
        {
            string loggerMethodType = inputInfo[0];
            string date = inputInfo[1];
            string message = inputInfo[2];

            if (loggerMethodType == "INFO")
            {
                logger.Info(date, message);
            }
            else if (loggerMethodType == "WARNING")
            {
                logger.Warning(date, message);
            }
            else if (loggerMethodType == "ERROR")
            {
                logger.Error(date, message);
            }
            else if (loggerMethodType == "CRITICAL")
            {
                logger.Critical(date, message);
            }
            else if (loggerMethodType == "FATAL")
            {
                logger.Fatal(date, message);
            }
        }
    }
}
