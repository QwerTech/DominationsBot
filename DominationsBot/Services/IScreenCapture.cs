using System.Drawing;

namespace DominationsBot.Services
{
    public interface IScreenCapture
    {
        Bitmap SnapShot();
        Bitmap SnapShot(Rectangle rect);
    }
}