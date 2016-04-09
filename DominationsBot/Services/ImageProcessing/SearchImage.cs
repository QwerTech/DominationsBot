using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace DominationsBot.Services.ImageProcessing
{
    public static class SearchImage
    {


        public static List<Point> GetSubPositions(Bitmap main, Bitmap sub)
        {
            List<Point> possiblepos = new List<Point>();

            int mainwidth = main.Width;
            int mainheight = main.Height;

            int subwidth = sub.Width;
            int subheight = sub.Height;

            int movewidth = mainwidth - subwidth;
            int moveheight = mainheight - subheight;

            BitmapData bmMainData = main.LockBits(new Rectangle(0, 0, mainwidth, mainheight), ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            BitmapData bmSubData = sub.LockBits(new Rectangle(0, 0, subwidth, subheight), ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            int bytesMain = Math.Abs(bmMainData.Stride) * mainheight;
            int strideMain = bmMainData.Stride;
            global::System.IntPtr scan0Main = bmMainData.Scan0;
            byte[] dataMain = new byte[bytesMain];
            global::System.Runtime.InteropServices.Marshal.Copy(scan0Main, dataMain, 0, bytesMain);

            int bytesSub = Math.Abs(bmSubData.Stride) * subheight;
            int strideSub = bmSubData.Stride;
            global::System.IntPtr scan0Sub = bmSubData.Scan0;
            byte[] dataSub = new byte[bytesSub];
            global::System.Runtime.InteropServices.Marshal.Copy(scan0Sub, dataSub, 0, bytesSub);

            for (int y = 0; y <= moveheight; ++y)
            {
                for (int x = 0; x <= movewidth; ++x)
                {
                    PixelData curcolor = GetColor(x, y, strideMain, dataMain);

                    foreach (var item in possiblepos.ToArray())
                    {
                        int xsub = x - item.X;
                        int ysub = y - item.Y;
                        if (xsub >= subwidth || ysub >= subheight || xsub < 0)
                            continue;

                        PixelData subcolor = GetColor(xsub, ysub, strideSub, dataSub);

                        if (!IsSubColor(subcolor, curcolor))
                        {
                            possiblepos.Remove(item);
                        }
                    }

                    var subColor = GetColor(0, 0, strideSub, dataSub);
                    if (curcolor.Equals(subColor))
                        possiblepos.Add(new Point(x, y));
                }
            }

            global::System.Runtime.InteropServices.Marshal.Copy(dataSub, 0, scan0Sub, bytesSub);
            sub.UnlockBits(bmSubData);

            global::System.Runtime.InteropServices.Marshal.Copy(dataMain, 0, scan0Main, bytesMain);
            main.UnlockBits(bmMainData);

            return possiblepos;
        }

        public static bool IsSubColor(PixelData mainPixel, PixelData subPixe)
        {
            if (mainPixel.MinR < subPixe.R && mainPixel.MaxR > subPixe.R
                && mainPixel.MinG < subPixe.G && mainPixel.MaxG > subPixe.G
                && mainPixel.MinB < subPixe.B && mainPixel.MaxB > subPixe.B)
                return true;
            return false;
        }

        private static PixelData GetColor(Point point, int stride, byte[] data)
        {
            return GetColor(point.X, point.Y, stride, data);
        }

        private static PixelData GetColor(int x, int y, int stride, byte[] data)
        {
            int pos = y * stride + x * 4;
            byte a = data[pos + 3];
            byte r = data[pos + 2];
            byte g = data[pos + 1];
            byte b = data[pos + 0];
            return new PixelData(a, r, g, b);
        }

        public struct PixelData
        {
            public byte A;
            public int MinR => GetMin(R);
            public int MaxR => GetMax(R);
            public byte R;
            public int MinG => GetMin(G);
            public int MaxG => GetMax(G);
            public byte G;
            public int MinB => GetMin(B);
            public int MaxB => GetMax(B);
            public byte B;

            public byte GetMin(byte input)
            {
                if (A == byte.MaxValue)
                    return input;
                var min = R - (byte.MaxValue - A);
                return min < 0 ? (byte)0 : (byte)min;
            }

            public byte GetMax(byte input)
            {
                if (A == byte.MaxValue)
                    return input;
                var max = R + (byte.MaxValue - A);
                return max > byte.MaxValue ? byte.MaxValue : (byte)max;
            }


            public PixelData(byte a, byte r, byte g, byte b)
            {
                A = a;
                R = r;
                G = g;
                B = b;
            }
        }
    }
}