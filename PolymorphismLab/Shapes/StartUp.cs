using System;

namespace Shapes
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Shape rectangle = new Rectangle(2, 3);
            Shape circle = new Circle(5);

            Console.WriteLine($"{rectangle.CalculateArea():F2}");
            Console.WriteLine($"{rectangle.CalculatePerimeter():F2}");
            Console.WriteLine(rectangle.Draw());

            Console.WriteLine($"{circle.CalculateArea():F2}");
            Console.WriteLine($"{circle.CalculatePerimeter():F2}");
            Console.WriteLine(circle.Draw());
        }
    }
}
