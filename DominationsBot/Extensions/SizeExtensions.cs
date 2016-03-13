using System.Drawing;

namespace DominationsBot.Extensions
{
    public static class SizeExtensions
    {
        public static Size Multiply(this Size size, int multiplier)
        {
            return new Size(new Point(size).Multiply(multiplier));
        }
    }
}