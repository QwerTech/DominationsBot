using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DominationsBot.Services
{
    public class ResizeEpsilonTemplateFinder : ResizeTemplateFinder
    {
        private readonly ITemplateMatching _templateMatching;
        private readonly BitmapPreparer _bitmapPreparer;

        private const int epsilon = 10;

        public override IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var tm = base.FindTemplate(bmp, template);

            return tm.Where(t =>
            {
                Rectangle tempRect = t.Rectangle;

                var result = Math.Abs(template.Width  - tempRect.Width) < epsilon
                             &&
                             Math.Abs(template.Height - tempRect.Height) < epsilon;
                return result;
            });
        }

        public ResizeEpsilonTemplateFinder(ITemplateMatching templateMatching, BitmapPreparer bitmapPreparer)
            : base(templateMatching, bitmapPreparer)
        {
        }
    }
}