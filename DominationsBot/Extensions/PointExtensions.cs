using System.Drawing;

namespace DominationsBot.Extensions
{
    public static class PointExtensions
    {
        public static Point Multiply(this Point point, int multiplier)
        {
            return new Point(point.X*multiplier,point.Y*multiplier);
            
        }
    }
}
