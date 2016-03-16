using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Imaging;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class ResizeEpsilonTemplateFinder : ResizeTemplateFinder
    {
        
        private const int Epsilon = 10;

        public override IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var tm = base.FindTemplate(bmp, template);

            return tm.Where(t =>
            {
                Rectangle tempRect = t.Rectangle;

                var result = Math.Abs(template.Width  - tempRect.Width) < Epsilon
                             &&
                             Math.Abs(template.Height - tempRect.Height) < Epsilon;
                return result;
            });
        }

        public ResizeEpsilonTemplateFinder(ITemplateMatching templateMatching, BitmapPreparer bitmapPreparer)
            : base(templateMatching, bitmapPreparer)
        {
        }
    }
}