using AForge.Imaging;
using AForge.Imaging.Filters;
using DominationsBot.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DominationsBot.Services
{
    public class ResizeTemplateFinder : ITemplateFinder
    {
        private readonly ITemplateMatching _templateMatching;
        private readonly BitmapPreparer _bitmapPreparer;

        public ResizeTemplateFinder(ITemplateMatching templateMatching, BitmapPreparer bitmapPreparer)
        {
            _templateMatching = templateMatching;
            _bitmapPreparer = bitmapPreparer;
        }

        public int Divisor => 4;

        public virtual IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            const int epsilon = 10;


            var newHeight = bmp.Height / Divisor;
            var newWidth = bmp.Width / Divisor;
            var tm = _templateMatching.ProcessImage(
                new ResizeNearestNeighbor(newWidth, newHeight).Apply(bmp),
                new ResizeNearestNeighbor(template.Width / Divisor, template.Height / Divisor).Apply(template),
                new Rectangle(0, 0, newWidth, newHeight)
                );
            return tm.Select(t => new TemplateMatchExt(t, Divisor));

        }

        public class TemplateMatchExt : TemplateMatch
        {

            public TemplateMatchExt(TemplateMatch match, int divisor) : base(match.Rectangle.Multiply(divisor), match.Similarity)
            {
            }
        }
    }
}