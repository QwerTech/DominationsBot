using BitmapDetector2;
using System.Collections.Generic;
using System.Drawing;
using AForge.Imaging;

namespace DominationsBot.Services
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
