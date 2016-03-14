using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;

namespace DominationsBot.Services
{
    public interface ITemplateFinder
    {
        IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template);
    }
}