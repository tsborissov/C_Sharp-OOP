using System;
using System.Collections.Generic;

namespace P02.Graphic_Editor
{
    class Program
    {
        static void Main()
        {
            List<IShape> shapes = new List<IShape>() {
                new Square(),
                new Rectangle(),
                new Circle(),
                new Triangle() };

            var graphicEditor = new GraphicEditor();

            foreach (var shape in shapes)
            {
                Console.WriteLine(graphicEditor.DrawShape(shape));
            }
        }
    }
}
