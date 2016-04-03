using System;
using AForge.Imaging;
using AForge.Imaging.Filters;
using DominationsBot.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class ResizeTemplateFinder : ITemplateFinder
    {
        private readonly ITemplateMatching _templateMatching;
        private readonly BitmapPreparer _bitmapPreparer;
        protected readonly Settings _settings;

        public ResizeTemplateFinder(ITemplateMatching templateMatching, BitmapPreparer bitmapPreparer, Settings settings)
        {
            _templateMatching = templateMatching;
            _bitmapPreparer = bitmapPreparer;
            _settings = settings;
        }

        private int Divisor => 2;

        public virtual IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            Trace.TraceInformation("Ищем совпадения");
            bmp = _bitmapPreparer.Prepare(bmp);
            template = _bitmapPreparer.Prepare(template);
            var newHeight = bmp.Height / Divisor;
            var newWidth = bmp.Width / Divisor;
            var tm = _templateMatching.ProcessImage(
                new ResizeNearestNeighbor(newWidth, newHeight).Apply(bmp),
                new ResizeNearestNeighbor(template.Width / Divisor, template.Height / Divisor).Apply(template),
                new Rectangle(0, 0, newWidth, newHeight)
                );
            Trace.TraceInformation($"Нашли совпадения {tm.Length}");

            var templateMatchExts = tm.Select(t => new TemplateMatchExt(t, Divisor)).ToList();
            bmp.ViewContains(templateMatchExts).Save(Path.Combine(_settings.LogsPath, $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}_resizeMatches.png"));
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

        public class TemplateMatchExt :  TemplateMatch
        {

            public TemplateMatchExt(TemplateMatch match, int divisor) : base(match.Rectangle.Multiply(divisor), match.Similarity)
            {
            }
        }
    }
}