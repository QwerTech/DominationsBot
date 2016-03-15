using System.Drawing;
using System.Drawing.Imaging;

namespace DominationsBot.Services.ImageProcessing
{
    public class BitmapPreparer
    {
        public Bitmap Prepare(Bitmap bitmap)
        {
            if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
                return bitmap;
            var targetBmp = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                PixelFormat.Format24bppRgb);
            return targetBmp;
        }
    }
}