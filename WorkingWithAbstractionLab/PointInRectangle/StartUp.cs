using System;
using System.Linq;
using System.Text;

namespace PointInRectangle
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] rectangleCoordinates = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Point topLeftCorner = new Point(rectangleCoordinates[0], rectangleCoordinates[1]);
            Point bottomRightCorner = new Point(rectangleCoordinates[2], rectangleCoordinates[3]);

            Rectangle rectangle = new Rectangle(topLeftCorner, bottomRightCorner);

            int n = int.Parse(Console.ReadLine());

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < n; i++)
            {
                int[] pointCoordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();

                Point pointToCheck = new Point(pointCoordinates[0], pointCoordinates[1]);

                result.AppendLine(rectangle.Contains(pointToCheck).ToString());
            }

            Console.WriteLine(result.ToString().TrimEnd());
        }
    }
}
