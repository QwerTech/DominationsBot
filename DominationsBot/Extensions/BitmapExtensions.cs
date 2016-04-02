using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
    }
}