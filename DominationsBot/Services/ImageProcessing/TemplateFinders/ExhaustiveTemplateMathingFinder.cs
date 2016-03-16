using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Imaging;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class ExhaustiveTemplateMathingFinder : ITemplateFinder
    {
        private readonly BitmapPreparer _bitmapPreparer;
        private readonly ExhaustiveTemplateMatching _exhaustiveTemplateMatching;

        public ExhaustiveTemplateMathingFinder(BitmapPreparer bitmapPreparer,
            ExhaustiveTemplateMatching exhaustiveTemplateMatching)
        {
            _bitmapPreparer = bitmapPreparer;
            _exhaustiveTemplateMatching = exhaustiveTemplateMatching;
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

        /// <summary>
        ///     See if bmp is contained in template with a small margin of error.
        /// </summary>
        /// <param name="template">The Bitmap that might contain.</param>
        /// <param name="bmp">The Bitmap that might be contained in.</param>
        /// <returns>You guess!</returns>
        public IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            bmp = _bitmapPreparer.Prepare(bmp);
            template = _bitmapPreparer.Prepare(template);
            var tm = _exhaustiveTemplateMatching.ProcessImage(bmp, template);
            return tm;
        }
    }
}