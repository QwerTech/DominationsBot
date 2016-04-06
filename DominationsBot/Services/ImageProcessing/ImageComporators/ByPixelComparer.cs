using System.Drawing;

namespace DominationsBot.Services.ImageProcessing.ImageComporators
{
    public class ByPixelComparer : IImageComparer
    {
        public bool Compare(Bitmap one, Bitmap another)
        {
            if (one.Width != another.Width || one.Height != another.Height)
                return false;

            for (var i = 0; i < one.Width; i++)
            {
                for (var j = 0; j < one.Height; j++)
                {
                    var img1Ref = one.GetPixel(i, j).ToString();
                    var img2Ref = another.GetPixel(i, j).ToString();
                    if (img1Ref != img2Ref)
                        return false;
                }
            }
            return true;
        }
    }
}