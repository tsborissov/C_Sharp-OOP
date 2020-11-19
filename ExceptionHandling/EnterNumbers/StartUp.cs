using System;

namespace EnterNumbers
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());
            
            for (int i = 1; i <= 10; i++)
            {
                try
                {
                    ReadNumber(start, end);
                }
                catch (Exception)
                {

                    i = 0;
                }

            }

            void ReadNumber(int start, int end)
            {
                string input = Console.ReadLine();

                bool isValidNumber = int.TryParse(input, out int number);
                bool isWithinRange = number >= start && number <= end;

                if (!isValidNumber || !isWithinRange)
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}
