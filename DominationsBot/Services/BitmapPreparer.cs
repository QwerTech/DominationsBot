using System.Drawing;
using System.Drawing.Imaging;

namespace DominationsBot.Services
{
    public class BitmapPreparer
    {
        public Bitmap Prepare(Bitmap bitmap)
        {
            var targetBmp = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                PixelFormat.Format24bppRgb);
            return targetBmp;
        }
    }
}