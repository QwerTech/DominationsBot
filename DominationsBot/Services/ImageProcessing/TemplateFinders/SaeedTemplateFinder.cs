using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using AForge.Imaging;
using DominationsBot.Extensions;
using Image = System.Drawing.Image;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class SaeedTemplateFinder : ITemplateFinder
    {
        private readonly Settings _settings;

        public SaeedTemplateFinder(Settings settings)
        {
            _settings = settings;
        }

        private readonly double _deviation;

        public SaeedTemplateFinder(double deviation)
        {
            _deviation = deviation;
        }

        public bool Exists(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Any();
        }

        public bool Single(Bitmap bmp, Bitmap template)
        {
            var templateMatches = FindTemplate(bmp, template);
            return templateMatches.Count() == 1;
        }

        public int Divisor => 1;

        public IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var search = SearchBitmap(bmp, template, _deviation);
            Trace.TraceInformation("Совпадений не найдено");
            if (search == Rectangle.Empty)
                return Enumerable.Empty<TemplateMatch>();

            var templateMatches = new[] { new TemplateMatch(search, 1) };
            bmp.ViewContains(templateMatches).Save(Path.Combine(_settings.LogsPath, $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}_SaeedTemplateFinderMatches.png"));
            return templateMatches;
        }

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
    }
}