using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

        public bool Exists(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Any();
        }

        public bool Single(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Count() == 1;
        }
    }
}