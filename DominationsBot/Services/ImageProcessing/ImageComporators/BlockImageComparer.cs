using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DominationsBot.Services.ImageProcessing.ImageComporators
{
    public class BlockImageComparer : IImageComparer
    {
        public bool Compare(Bitmap one, Bitmap another)
        {
            return Compare(one, another, new Size(20, 20)).Count == 0;
        }
        

        public static List<Rectangle> Compare(Bitmap bmp1, Bitmap bmp2, Size block)
        {
            var rects = new List<Rectangle>();
            var pf = PixelFormat.Format24bppRgb;

            var bd1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, pf);
            var bd2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, pf);

            try
            {
                unsafe
                {
                    int w = 0, h = 0;

                    while (h < bd1.Height && h < bd2.Height)
                    {
                        var p1 = (byte*) bd1.Scan0 + h*bd1.Stride;
                        var p2 = (byte*) bd2.Scan0 + h*bd2.Stride;

                        w = 0;
                        while (w < bd1.Width && w < bd2.Width)
                        {
 
                            for (var i = 0; i < block.Width; i++)
                            {
                                var wi = w + i;
                                if (wi >= bd1.Width || wi >= bd2.Width) break;

                                for (var j = 0; j < block.Height; j++)
                                {
                                    var hj = h + j;
                                    if (hj >= bd1.Height || hj >= bd2.Height) break;

                                    var pc1 = (IcColor*) (p1 + wi*3 + bd1.Stride*j);
                                    var pc2 = (IcColor*) (p2 + wi*3 + bd2.Stride*j);

                                    if (pc1->R != pc2->R || pc1->G != pc2->G || pc1->B != pc2->B)
                                    {
 
                                        var bw = Math.Min(block.Width, bd1.Width - w);
                                        var bh = Math.Min(block.Height, bd1.Height - h);
                                        rects.Add(new Rectangle(w, h, bw, bh));

                                        goto E;
                                    }
                                }
                            }
                            E:
                            w += block.Width;
                        }

                        h += block.Height;
                    }
                }
            }
            finally
            {
                bmp1.UnlockBits(bd1);
                bmp2.UnlockBits(bd2);
            }

            return rects;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct IcColor
        {
            [FieldOffset(0)] public readonly byte B;
            [FieldOffset(1)] public readonly byte G;
            [FieldOffset(2)] public readonly byte R;
        }
    }
}