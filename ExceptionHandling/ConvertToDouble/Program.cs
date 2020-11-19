using System;

namespace ConvertToDouble
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            double result;

            try
            {
                result = Convert.ToDouble(input);
                Console.WriteLine(result);
            }
            catch (FormatException)
            {
                Console.WriteLine($"{input} is not in an appropriate format for a Double type.");
            }
            catch (InvalidCastException)
            {
                Console.WriteLine($"{input} does not implement the IConvertible interface.");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"{input} represents a number that is less than MinValue or greater than MaxValue.");
            }
        }
    }
}
