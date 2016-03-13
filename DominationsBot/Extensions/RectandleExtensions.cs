using System.Drawing;

namespace DominationsBot.Extensions
{
    public static class RectandleExtensions
    {
        public static Point Middle(this Rectangle rectangle)
        {
            return new Point(rectangle.X + rectangle.Width/2, rectangle.Y + rectangle.Height/2);
        }

        public static Rectangle Multiply(this Rectangle rectangle, int multiplier)
        {
            return new Rectangle(rectangle.Location.Multiply(multiplier), rectangle.Size.Multiply(multiplier));
        }
    }
}