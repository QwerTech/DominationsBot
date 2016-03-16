using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace DominationsBot.Services.ImageProcessing
{
    public static class SearchImage
    {
        public static unsafe Rectangle SearchBitmap(Bitmap bigBmp, Bitmap smallBmp, double deviation)
        {
            BitmapData smallBmpData = smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData bigBmpData = bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int smallStride = smallBmpData.Stride;
            int bigStride = bigBmpData.Stride;
            int widthInPixels = bigBmp.Width;
            int lookingHeight = bigBmp.Height - smallBmp.Height + 1;
            var bytesPerPixel = Image.GetPixelFormatSize(bigBmp.PixelFormat) / 8;
            int bytesInLineOfSmallBmp = smallBmp.Width * bytesPerPixel;
            int smallBmpHeightInPixels = smallBmp.Height;
            Rectangle rectangle = Rectangle.Empty;
            int byteDeviation = Convert.ToInt32((double)byte.MaxValue * deviation);
            byte* smallBmpPointer = (byte*)smallBmpData.Scan0.ToPointer();
            byte* bigBmpPointer = (byte*)bigBmpData.Scan0.ToPointer();
            int num5 = bigStride - bigBmp.Width * bytesPerPixel;
            bool templateFound = true;
            for (int lineNumber = 0; lineNumber < lookingHeight; ++lineNumber)
            {
                for (int linePixelNumber = 0; linePixelNumber < widthInPixels; ++linePixelNumber)
                {
                    byte* numPtr3 = bigBmpPointer;
                    byte* numPtr4 = smallBmpPointer;
                    for (int heightIndex = 0; heightIndex < smallBmpHeightInPixels; ++heightIndex)
                    {
                        templateFound = true;
                        for (int byteInSmallBmlLine = 0; byteInSmallBmlLine < bytesInLineOfSmallBmp; ++byteInSmallBmlLine)
                        {
                            if ((int)*bigBmpPointer + byteDeviation < (int)*smallBmpPointer ||
                                (int)*bigBmpPointer - byteDeviation > (int)*smallBmpPointer)
                            {
                                templateFound = false;
                                break;
                            }
                            ++bigBmpPointer;
                            ++smallBmpPointer;
                        }
                        if (templateFound)
                        {
                            smallBmpPointer = numPtr4 + (smallStride * (1 + heightIndex));
                            bigBmpPointer = numPtr3 + (bigStride * (1 + heightIndex));
                        }
                        else
                            break;
                    }
                    if (templateFound)
                    {
                        rectangle.X = linePixelNumber;
                        rectangle.Y = lineNumber;
                        rectangle.Width = smallBmp.Width;
                        rectangle.Height = smallBmp.Height;
                        break;
                    }
                    smallBmpPointer = numPtr4;
                    bigBmpPointer = numPtr3 + bytesPerPixel;
                }
                if (!templateFound)
                    bigBmpPointer += num5;
                else
                    break;
            }
            bigBmp.UnlockBits(bigBmpData);
            smallBmp.UnlockBits(smallBmpData);
            return rectangle;
        }

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
            global::System.IntPtr Scan0Main = bmMainData.Scan0;
            byte[] dataMain = new byte[bytesMain];
            global::System.Runtime.InteropServices.Marshal.Copy(Scan0Main, dataMain, 0, bytesMain);

            int bytesSub = Math.Abs(bmSubData.Stride) * subheight;
            int strideSub = bmSubData.Stride;
            global::System.IntPtr Scan0Sub = bmSubData.Scan0;
            byte[] dataSub = new byte[bytesSub];
            global::System.Runtime.InteropServices.Marshal.Copy(Scan0Sub, dataSub, 0, bytesSub);

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

            global::System.Runtime.InteropServices.Marshal.Copy(dataSub, 0, Scan0Sub, bytesSub);
            sub.UnlockBits(bmSubData);

            global::System.Runtime.InteropServices.Marshal.Copy(dataMain, 0, Scan0Main, bytesMain);
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