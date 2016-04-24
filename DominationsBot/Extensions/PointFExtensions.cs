using System;
using System.Drawing;

namespace DominationsBot.Extensions
{
    public static class PointFExtensions
    {
        public static Point ToPoint(this PointF pointF)
        {
            return new Point((int)Math.Round(pointF.X), (int)Math.Round(pointF.Y));
        }
    }
    public static class sizeFExtensions
    {
        public static SizeF Rotate90(this SizeF sizeF)
        {
            float newWidth= sizeF.Width;
            float newHeight= sizeF.Height;
            if (sizeF.Width > 0 && sizeF.Height >0)
                newWidth = -sizeF.Width;
            if (sizeF.Width < 0 && sizeF.Height > 0)
                newHeight = -sizeF.Height;
            if (sizeF.Width < 0 && sizeF.Height < 0)
                newWidth = -sizeF.Width;
            if (sizeF.Width > 0 && sizeF.Height < 0)
                newHeight= -sizeF.Height;
            return new SizeF(newWidth, newHeight);
        }
    }
}