using SOLID.Enums;
using SOLID.Layouts;

namespace SOLID.Appenders
{
    public abstract class Appender : IAppender
    {
        public Appender(ILayout layout)
        {
            this.Layout = layout;
        }
        
        public ILayout Layout { get; }

        public ReportLevel ReportLevel { get; set; }

        protected int Counter { get; set; }

        public abstract void Append(string dateTime, ReportLevel errorLevel, string message);

        public override string ToString()
        {
            //Appender type: ConsoleAppender, Layout type: SimpleLayout, Report level: CRITICAL,
            

            return $"Appender type: {this.GetType().Name}, Layout type: {this.Layout.GetType().Name}, Report level: {this.ReportLevel.ToString().ToUpper()}, Messages appended: {this.Counter}";
        }

    }
}
