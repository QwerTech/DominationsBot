using AForge.Imaging;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DominationsBot.Services.ImageProcessing
{
    public class SaeedTemplateFinder : ITemplateFinder
    {
        private readonly double _deviation;

        public SaeedTemplateFinder(double deviation)
        {
            _deviation = deviation;
        }

        public bool Exists(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Any();
        }

        public bool Single(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Count() == 1;
        }

        public int Divisor => 1;

        public IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var search = SearchImage.SearchBitmap(bmp, template, _deviation);
            if (search == Rectangle.Empty)
                return Enumerable.Empty<TemplateMatch>();
            return new[] { new TemplateMatch(search, 1) };
        }
    }
}