using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AForge.Imaging;
using DominationsBot.Extensions;
using DominationsBot.Services.Logging;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class ExhaustiveTemplateMathingFinder : ITemplateFinder
    {
        private readonly BitmapPreparer _bitmapPreparer;
        private readonly ExhaustiveTemplateMatching _exhaustiveTemplateMatching;
        private readonly ImageLogger _imageLogger;

        public ExhaustiveTemplateMathingFinder(BitmapPreparer bitmapPreparer,
            ExhaustiveTemplateMatching exhaustiveTemplateMatching, ImageLogger imageLogger)
        {
            _bitmapPreparer = bitmapPreparer;
            _exhaustiveTemplateMatching = exhaustiveTemplateMatching;
            _imageLogger = imageLogger;
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
            _imageLogger.Log(bmp.ViewContains(tm), "ExhaustiveTemplateMatches");
            return tm;
        }
    }
}