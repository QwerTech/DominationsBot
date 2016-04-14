using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using AForge.Imaging;
using Castle.Core.Internal;

namespace DominationsBot.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap ViewContains(this Bitmap bmp, IEnumerable<TemplateMatch> matches)
        {
            var data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);
            foreach (var m in matches)
            {
                bmp.CheckRectangle(m.Rectangle);
                Drawing.Rectangle(data, m.Rectangle, Color.Red);
                // do something else with matching
            }
            bmp.UnlockBits(data);
            return bmp;
        }

        public static Bitmap DrawRectangle(this Bitmap bmp, Rectangle rectangle)
        {
            bmp.CheckRectangle(rectangle);
            var data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);

            Drawing.Rectangle(data, rectangle, Color.Red);
            bmp.UnlockBits(data);
            return bmp;
        }

        public static Bitmap DrawPointPosition(this Bitmap bmp, Point point)
        {
            var up = point.Y - 5;
            var left = point.X - 5;
            if (up < 0)
                up = +10;
            if (left < 0)
                left += 10;
            var points = new[] {new Point(point.X, up), point, new Point(left, point.Y)};
            points.ForEach(bmp.CheckPoint);
            var graphics = Graphics.FromImage(bmp);
            graphics.DrawLines(new Pen(Color.Red),points);
            
            return bmp;
        }

        public static void DrawString(this Bitmap bitmap, string text, Point point)
        {
            bitmap.CheckPoint(point);
            var graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.DrawString(text, new Font(FontFamily.GenericMonospace, 8), Brushes.Red, point);
            graphics.Flush();
        }
        public static void CheckPoint(this Bitmap bitmap, Point point)
        {
            if (!new Rectangle(new Point(), bitmap.Size).Contains(point))
                throw new ArgumentOutOfRangeException($"Точка {point} не внутри изображения c разрмерами {bitmap.Size}");
        }
        public static void CheckRectangle(this Bitmap bitmap, Rectangle rectangle)
        {
            if (!new Rectangle(new Point(), bitmap.Size).Contains(rectangle))
                throw new ArgumentOutOfRangeException($"Прямоугольник {rectangle} не внутри изображения c разрмерами {bitmap.Size}");
        }
        public static bool Compare(this Color color, Color other)
        {
            return color == other ||
                   (color.R == other.R && color.B == other.B && color.G == other.G);
        }
    }
}