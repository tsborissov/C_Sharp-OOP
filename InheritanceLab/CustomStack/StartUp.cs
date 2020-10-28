using System;
using System.Collections.Generic;

namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var myCoolStack = new StackOfStrings();

            Console.WriteLine(myCoolStack.IsEmpty());

            myCoolStack.AddRange(new List<string> { "1", "2", "3" });

            Console.WriteLine(string.Join(" ", myCoolStack));

            Console.WriteLine(myCoolStack.IsEmpty());

            myCoolStack.AddRange(new List<string> { "4", "5", "6" });

            Console.WriteLine(string.Join(" ", myCoolStack));

        }
    }
}
