using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using AForge.Imaging;
using AForge.Imaging.Filters;
using DominationsBot.Extensions;
using DominationsBot.Services.Logging;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class ResizeTemplateFinder : ITemplateFinder
    {
        private readonly BitmapPreparer _bitmapPreparer;
        private readonly ImageLogger _imageLogger;
        private readonly ITemplateMatching _templateMatching;

        public ResizeTemplateFinder(ITemplateMatching templateMatching, BitmapPreparer bitmapPreparer,
            ImageLogger imageLogger)
        {
            _templateMatching = templateMatching;
            _bitmapPreparer = bitmapPreparer;
            _imageLogger = imageLogger;
        }

        private int Divisor => 2;

        public virtual IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            Trace.TraceInformation("Ищем совпадения");
            bmp = _bitmapPreparer.Prepare(bmp);
            template = _bitmapPreparer.Prepare(template);
            var newHeight = bmp.Height/Divisor;
            var newWidth = bmp.Width/Divisor;
            var tm = _templateMatching.ProcessImage(
                new ResizeNearestNeighbor(newWidth, newHeight).Apply(bmp),
                new ResizeNearestNeighbor(template.Width/Divisor, template.Height/Divisor).Apply(template),
                new Rectangle(0, 0, newWidth, newHeight)
                );
            Trace.TraceInformation($"Нашли совпадения {tm.Length}");

            var templateMatchExts = tm.Select(t => new TemplateMatchExt(t, Divisor)).ToList();
            _imageLogger.Log(bmp.ViewContains(templateMatchExts), "resizeMatches");
            return templateMatchExts;
        }

        public bool Exists(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Any();
        }

        public bool Single(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Count() == 1;
        }

        public class TemplateMatchExt : TemplateMatch
        {
            public TemplateMatchExt(TemplateMatch match, int divisor)
                : base(match.Rectangle.Multiply(divisor), match.Similarity)
            {
            }
        }
    }
}