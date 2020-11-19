using System;

namespace SqareRoot
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();

            bool isValidInteger = int.TryParse(input, out int number);

            try
            {
                if (!isValidInteger || number < 0)
                {
                    throw new ArgumentException("Invalid number");
                }

                double result = Math.Sqrt(number);

                Console.WriteLine(result);
            }
            catch (Exception ae)
            {
                Console.WriteLine(ae.Message);
            }
            finally
            {
                Console.WriteLine("Good bye");
            }
        }
    }
}
