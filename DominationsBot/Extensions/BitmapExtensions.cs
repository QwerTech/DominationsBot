using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;

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
                Drawing.Rectangle(data, m.Rectangle, Color.Red);
                // do something else with matching
            }
            bmp.UnlockBits(data);
            return bmp;
        }

        public static bool Compare(this Color color, Color other)
        {
            return color == other || 
                (color.R == other.R && color.B == other.B && color.G == other.G);
        }
    }
}