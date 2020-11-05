using System;

namespace ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get => this.length;
            
            private set
            {
                ValidateBoxSide(value, nameof(this.Length));

                this.length = value;
            }
        }

        public double Width
        { 
            get => this.width; 
            private set
            {
                ValidateBoxSide(value, nameof(this.Width));

                this.width = value;
            } 
        }

        public double Height
        {
            get => this.height;
            private set
            {
                ValidateBoxSide(value, nameof(this.Height));

                this.height = value;
            }
        }

        private void ValidateBoxSide(double side, string sideName)
        {
            if (side <= 0)
            {
                throw new ArgumentException($"{sideName} cannot be zero or negative.");
            }
        }

        public double SurfaceArea()
        {
            double surfaceArea = 2 * this.length * this.width + 2 * this.length * this.height + 2 * this.width * this.height;
            return surfaceArea;
        }

        public double LateralSurfaceArea()
        {
            double lateralSurfaceArea = 2 * this.length * this.height + 2 * this.width * this.height;
            return lateralSurfaceArea;
        }

        public double Volume()
        {
            double volume = this.length * this.width * this.height;
            return volume;
        }


    }
}
