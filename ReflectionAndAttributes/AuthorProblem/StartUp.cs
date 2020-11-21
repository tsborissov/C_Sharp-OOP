using System;

namespace AuthorProblem
{
    [Author("Ventsi")]
    public class StartUp
    {
        [Author("Gosho")]
        public static void Main()
        {
            var tracker = new Tracker();

            Console.WriteLine(tracker.PrintMethodsByAuthor());
        }
    }
}
