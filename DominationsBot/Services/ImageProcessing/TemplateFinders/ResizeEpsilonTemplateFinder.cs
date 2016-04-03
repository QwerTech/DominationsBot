using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AForge.Imaging;
using DominationsBot.Extensions;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class ResizeEpsilonTemplateFinder : ResizeTemplateFinder
    {
        
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
            bmp.ViewContains(templateMatches).Save(Path.Combine(_settings.LogsPath, $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}_resizeEpsilonMatches.png"));
            return templateMatches;
        }

        public ResizeEpsilonTemplateFinder(ITemplateMatching templateMatching, BitmapPreparer bitmapPreparer, Settings settings)
            : base(templateMatching, bitmapPreparer,settings)
        {
        }
    }
}