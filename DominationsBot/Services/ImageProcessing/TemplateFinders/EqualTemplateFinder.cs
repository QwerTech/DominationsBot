using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Imaging;
using DominationsBot.Extensions;
using DominationsBot.Services.Logging;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class EqualTemplateFinder : ITemplateFinder
    {
        private readonly ImageLogger _imageLogger;


        public EqualTemplateFinder(ImageLogger imageLogger)
        {
            _imageLogger = imageLogger;
        }

        public IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var search =
                SearchImage.GetSubPositions(bmp, template)
                    .Select(p => new TemplateMatch(new Rectangle(p, template.Size), 1)).ToList();
            
            _imageLogger.Log(bmp.ViewContains(search), "EqualTemplateFinderMatches");
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