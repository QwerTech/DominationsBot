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
    public class ResizeEpsilonTemplateFinder : ResizeTemplateFinder
    {
        private readonly ImageLogger _imageLogger;

        private const int Epsilon = 10;

        public override IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var tm = base.FindTemplate(bmp, template);

            var templateMatches = tm.Where(t =>
            {
                Rectangle tempRect = t.Rectangle;

                var result = Math.Abs(template.Width  - tempRect.Width) < Epsilon
                             &&
                             Math.Abs(template.Height - tempRect.Height) < Epsilon;
                return result;
            }).ToList();
            _imageLogger.Log(bmp.ViewContains(templateMatches), "resizeEpsilonMatches");
            return templateMatches;
        }

        public ResizeEpsilonTemplateFinder(ITemplateMatching templateMatching, BitmapPreparer bitmapPreparer, ImageLogger imageLogger)
            : base(templateMatching, bitmapPreparer,imageLogger)
        {
            _imageLogger = imageLogger;
        }
    }
}