using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DominationsBot.Services.ImageProcessing.ImageComporators
{
    public class Base64StringComparer : IImageComparer
    {
        public bool Compare(Bitmap one, Bitmap another)
        {
            var ms = new MemoryStream();
            one.Save(ms, ImageFormat.Png);
            var firstBitmap = Convert.ToBase64String(ms.ToArray());
            ms.Position = 0;

            another.Save(ms, ImageFormat.Png);
            var secondBitmap = Convert.ToBase64String(ms.ToArray());

            return firstBitmap.Equals(secondBitmap);
        }
    }
}