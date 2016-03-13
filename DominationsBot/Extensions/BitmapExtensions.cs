using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;

namespace DominationsBot.Extensions
{
    public static class BitmapExtensions
    {
        /// <summary>
        ///     See if bmp is contained in template with a small margin of error.
        /// </summary>
        /// <param name="template">The Bitmap that might contain.</param>
        /// <param name="bmp">The Bitmap that might be contained in.</param>
        /// <returns>You guess!</returns>
        public static TemplateMatch[] FindTemplate(this Bitmap template, Bitmap bmp)
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

        public static Bitmap ViewContains(this Bitmap bmp, Bitmap template)
        {
            var matchings = bmp.FindTemplate(template);
            var data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);
            foreach (var m in matchings)
            {
                Drawing.Rectangle(data, m.Rectangle, Color.Red);
                // do something else with matching
            }
            bmp.UnlockBits(data);
            return bmp;
        }
    }
}