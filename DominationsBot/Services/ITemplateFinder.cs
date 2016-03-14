using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;

namespace DominationsBot.Services
{
    public interface ITemplateFinder
    {
        int Divisor { get; }
        IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template);
    }
}