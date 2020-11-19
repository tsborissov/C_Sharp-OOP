namespace SOLID.Loggers
{
    public interface ILogFile
    {
        public int Size { get; }
        public void Write(string message);
    }
}
