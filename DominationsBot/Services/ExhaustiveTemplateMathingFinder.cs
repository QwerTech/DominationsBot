using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace DominationsBot.Services
{
    public class ExhaustiveTemplateMathingFinder : ITemplateFinder
    {
        public int Divisor => 1;

        /// <summary>
        ///     See if bmp is contained in template with a small margin of error.
        /// </summary>
        /// <param name="template">The Bitmap that might contain.</param>
        /// <param name="bmp">The Bitmap that might be contained in.</param>
        /// <returns>You guess!</returns>
        public IEnumerable<TemplateMatch> FindTemplate(Bitmap template, Bitmap bmp)
        {
            var etm = new ExhaustiveTemplateMatching(0.8f);
            TemplateMatch[] tm;
            using (
                var targetBmp = template.Clone(new Rectangle(0, 0, template.Width, template.Height),
                    PixelFormat.Format24bppRgb))
            {
                tm = etm.ProcessImage(targetBmp, bmp);
            }


            return tm;
        }
    }
}