using SOLID.Enums;
using SOLID.Layouts;

namespace SOLID.Appenders
{
    public class ConsoleAppender : Appender
    {
        public ConsoleAppender(ILayout layout) 
            : base(layout)
        {

        }

        public override void Append(string dateTime, ReportLevel reportLevel, string message)
        {
            if (reportLevel >= this.ReportLevel)
            {
                this.Counter++;

                System.Console.WriteLine(this.Layout.Format, dateTime, reportLevel, message); ;
            }
        }
    }
}
