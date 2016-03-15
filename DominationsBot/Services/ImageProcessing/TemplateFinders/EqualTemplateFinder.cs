using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Imaging;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class EqualTemplateFinder : ITemplateFinder
    {

        public IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var search =
                SearchImage.GetSubPositions(bmp, template)
                    .Select(p => new TemplateMatch(new Rectangle(p, template.Size), 1));
            return search;
        }
    }
}