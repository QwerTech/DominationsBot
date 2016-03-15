using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public interface ITemplateFinder
    {
        IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template);
    }
}