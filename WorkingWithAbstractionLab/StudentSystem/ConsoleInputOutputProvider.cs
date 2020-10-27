using System;

namespace StudentSystem
{
    public class ConsoleInputOutputProvider : IInputOutputProvider
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }

        public void ShowOutput(string data)
        {
            Console.WriteLine(data);
        }
    }
}
