using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Imaging.Filters;

namespace DominationsBot.Services.ImageProcessing
{
    public class ColourFilter
    {
        public void ApplyFilter(Bitmap bitmap)
        {
            // create filter
            ColorFiltering filter = new ColorFiltering
            {
                Red = new IntRange(90, 255),
                Green = new IntRange(100, 255),
                Blue = new IntRange(100, 230)
            };
            // set color ranges to keep
            // apply the filter
            filter.ApplyInPlace(bitmap);
        }
    }
}
