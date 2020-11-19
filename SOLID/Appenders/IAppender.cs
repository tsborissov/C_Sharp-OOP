using SOLID.Enums;
using SOLID.Layouts;

namespace SOLID.Appenders
{
    public interface IAppender
    {
        public ILayout Layout { get; }

        public ReportLevel ReportLevel { get; set; }

        public void Append(string dateTime, ReportLevel errorLevel, string message);

    }
}
