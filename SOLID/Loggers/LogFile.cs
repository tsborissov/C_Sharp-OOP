using System;
using System.IO;
using System.Linq;

namespace SOLID.Loggers
{
    class LogFile : ILogFile
    {
        private const string LogFilePath = "../../../log.txt";

        public int Size
            => File.ReadAllText(LogFilePath).Where(char.IsLetter).Sum(x => x);

        public void Write(string message)
        {
            File.AppendAllText(LogFilePath, message + Environment.NewLine);
        }

       
    }
}
