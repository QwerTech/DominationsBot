using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;
using DominationsBot.Services.ImageProcessing.TemplateFinders;

namespace DominationsBot.Services.ImageProcessing
{
    public class SaeedTemplateFinder : ITemplateFinder
    {

        public int Divisor => 1;
        public IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            int tot = 0;
            var search = SearchImage.Search(ref bmp, ref template, ref tot);
            return new TemplateMatch[] { new TemplateMatch(new Rectangle(search, Size.Empty), 1) };
        }
    }
}
