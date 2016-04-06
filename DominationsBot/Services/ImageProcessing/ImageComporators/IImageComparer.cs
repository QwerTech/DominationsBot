using System.Drawing;

namespace DominationsBot.Services.ImageProcessing.ImageComporators
{
    public interface IImageComparer
    {
        bool Compare(Bitmap one, Bitmap another);
    }

}