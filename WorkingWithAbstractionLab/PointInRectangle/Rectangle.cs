namespace PointInRectangle
{
    public class Rectangle
    {
        public Rectangle(Point topLeftCorner, Point bottomRightCorner)
        {
            this.TopLeftCorner = topLeftCorner;
            this.BottomRightCorner = bottomRightCorner;
        }

        public Point TopLeftCorner { get; set; }
        public Point BottomRightCorner { get; set; }

        public bool Contains(Point point)
        {
            bool isXInside = false;
            bool isYInside = false;

            if (this.TopLeftCorner.X < this.BottomRightCorner.X)
            {
                isXInside =
                point.X >= this.TopLeftCorner.X &&
                point.X <= this.BottomRightCorner.X;
            }
            else
            {
                isXInside =
                point.X <= this.TopLeftCorner.X &&
                point.X >= this.BottomRightCorner.X;
            }
            
            if (this.TopLeftCorner.Y > this.BottomRightCorner.Y)
            {
                isYInside =
                point.Y <= this.TopLeftCorner.Y &&
                point.Y >= this.BottomRightCorner.Y;
            }
            else
            {
                isYInside =
                point.Y >= this.TopLeftCorner.Y &&
                point.Y <= this.BottomRightCorner.Y;
            }
            return isXInside && isYInside;
        }

    }
}
