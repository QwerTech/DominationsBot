using System.Drawing;
using AForge.Math.Geometry;
using DominationsBot.Extensions;

namespace DominationsBot.Services.ImageProcessing
{
    public class PictureTester
    {
        public bool TestPixel(Bitmap bitmap, Point pixel, Color expectedColor)
        {
            if (!new Rectangle(0, 0, bitmap.Width, bitmap.Height).Contains(pixel))
                return false;
            var color = bitmap.GetPixel(pixel.X, pixel.Y);
            return expectedColor.Equals(color);
        }

        public bool TestLine(Bitmap bitmap, LineSegment line, Color expectedColor)
        {
            var fromPoints = Line.FromPoints(line.Start, line.End);
            if (fromPoints.IsHorizontal)
                for (var i = 0; i < line.End.X - line.Start.X; i++)
                {
                    var x = (int) line.Start.X + i;
                    var pixel = bitmap.GetPixel(x, (int)line.Start.Y);
                    if (!expectedColor.Equals(pixel))
                        return false;
                }
            else
            {
                for (var i = 0; i < line.End.Y - line.Start.Y; i++)
                {
                    var y = (int)line.Start.Y + i;
                    var pixel = bitmap.GetPixel((int) line.Start.X , y);
                    if (!expectedColor.Compare(pixel))
                        return false;
                }
            }
            return true;
        }

        public bool TestArea(Bitmap bitmap, Rectangle rectangle, Color expectedColor)
        {
            for (var i = 0; i < rectangle.Width; i++)
            {
                var up = new AForge.Point(rectangle.X + i, rectangle.Y);
                var down = new AForge.Point(rectangle.X + i, rectangle.Bottom);
                if (!TestLine(bitmap,
                    new LineSegment(up,
                        down), expectedColor))
                    return false;
            }
            return true;
        }
    }
}