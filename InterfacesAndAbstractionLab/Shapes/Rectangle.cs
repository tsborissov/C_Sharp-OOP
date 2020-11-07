using System;

namespace Shapes
{
    public class Rectangle : IDrawable
    {
        private int width;
        private int height;

        public Rectangle(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width
        { 
            get => this.width; 
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(this.Width)} cannot be negative.");
                }

                this.width = value;
            }
        }

        public int Height 
        {
            get => this.height; 
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"{nameof(this.Height)} cannot be negative.");
                }

                this.height = value;
            }
        }

        public void Draw()
        {
            DrawLine(this.width, '*', '*');

            for (int i = 1; i < this.height; ++i)
            {
                DrawLine(this.width, '*', ' ');
            }

            DrawLine(this.width, '*', '*');
        }

        private void DrawLine(int width, char end, char mid)
        {
            Console.Write(end);

            for (int i = 1; i < width -1; ++i)
            {
                Console.Write(mid);
            }

            Console.WriteLine(end);
        }
    }
}
