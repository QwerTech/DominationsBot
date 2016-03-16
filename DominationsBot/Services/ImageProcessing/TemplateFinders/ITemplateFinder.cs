using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public interface ITemplateFinder
    {
        IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template);
        bool Exists(Bitmap bmp, Bitmap template);
        bool Single(Bitmap bmp, Bitmap template);
    }
}