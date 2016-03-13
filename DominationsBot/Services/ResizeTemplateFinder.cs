using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace DominationsBot.Services
{
    public class ResizeTemplateFinder
    {
        private readonly ITemplateMatching _templateMatching;
        private readonly BitmapPreparer _bitmapPreparer;

        public ResizeTemplateFinder(ITemplateMatching templateMatching,BitmapPreparer bitmapPreparer)
        {
            _templateMatching = templateMatching;
            _bitmapPreparer = bitmapPreparer;
        }

        public  int Divisor = 4;
        public TemplateMatch[] FindTemplate(Bitmap bmp, Bitmap template)
        {
         
            const int epsilon = 10;
            

            var newHeight = bmp.Height/Divisor;
            var newWidth = bmp.Width/Divisor;
            var tm = _templateMatching.ProcessImage(
                new ResizeNearestNeighbor(newWidth, newHeight).Apply(bmp),
                new ResizeNearestNeighbor(template.Width/Divisor, template.Height/Divisor).Apply(template),
                new Rectangle(0, 0, newWidth, newHeight)
                );

            return tm;
        }
    }
}