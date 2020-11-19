using SOLID.Enums;
using SOLID.Layouts;
using SOLID.Loggers;

namespace SOLID.Appenders
{
    class FileAppender : Appender
    {
        private ILogFile logFile;
        
        public FileAppender(ILayout layout, ILogFile logFile)
            : base(layout)
        {
            this.logFile = logFile;
        }
        
        public ILogFile LogFile { get; }

        public override void Append(string dateTime, ReportLevel reportLevel, string message)
        {
            if (reportLevel >= this.ReportLevel)
            {
                this.Counter++;

                this.logFile.Write(string.Format(this.Layout.Format, dateTime, reportLevel, message));
            }
        }

        public override string ToString()
        {
            return base.ToString() + $", File size: {this.logFile.Size}";
        }
    }
}
