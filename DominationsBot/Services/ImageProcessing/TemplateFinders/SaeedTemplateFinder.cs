using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using AForge.Imaging;
using DominationsBot.Extensions;
using DominationsBot.Services.Logging;
using Image = System.Drawing.Image;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class SaeedTemplateFinder : ITemplateFinder
    {
        private readonly double _deviation;
        private readonly ImageLogger _imageLogger;

        public SaeedTemplateFinder(ImageLogger imageLogger)
        {
            _imageLogger = imageLogger;
        }

        public SaeedTemplateFinder(double deviation)
        {
            _deviation = deviation;
        }

        public int Divisor => 1;

        public bool Exists(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Any();
        }

        public bool Single(Bitmap bmp, Bitmap template)
        {
            var templateMatches = FindTemplate(bmp, template);
            return templateMatches.Count() == 1;
        }

        public IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var search = SearchBitmap(bmp, template, _deviation);
            Trace.TraceInformation("Совпадений не найдено");
            if (search == Rectangle.Empty)
                return Enumerable.Empty<TemplateMatch>();

            var templateMatches = new[] {new TemplateMatch(search, 1)};

            _imageLogger.Log(bmp.ViewContains(templateMatches), "SaeedTemplateFinderMatches");

            return templateMatches;
        }

        public static unsafe Rectangle SearchBitmap(Bitmap bigBmp, Bitmap smallBmp, double deviation)
        {
            var smallBmpData = smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var bigBmpData = bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var smallStride = smallBmpData.Stride;
            var bigStride = bigBmpData.Stride;
            var widthInPixels = bigBmp.Width;
            var lookingHeight = bigBmp.Height - smallBmp.Height + 1;
            var bytesPerPixel = Image.GetPixelFormatSize(bigBmp.PixelFormat)/8;
            var bytesInLineOfSmallBmp = smallBmp.Width*bytesPerPixel;
            var smallBmpHeightInPixels = smallBmp.Height;
            var rectangle = Rectangle.Empty;
            var byteDeviation = Convert.ToInt32(byte.MaxValue*deviation);
            var smallBmpPointer = (byte*) smallBmpData.Scan0.ToPointer();
            var bigBmpPointer = (byte*) bigBmpData.Scan0.ToPointer();
            var num5 = bigStride - bigBmp.Width*bytesPerPixel;
            var templateFound = true;
            for (var lineNumber = 0; lineNumber < lookingHeight; ++lineNumber)
            {
                for (var linePixelNumber = 0; linePixelNumber < widthInPixels; ++linePixelNumber)
                {
                    var numPtr3 = bigBmpPointer;
                    var numPtr4 = smallBmpPointer;
                    for (var heightIndex = 0; heightIndex < smallBmpHeightInPixels; ++heightIndex)
                    {
                        templateFound = true;
                        for (var byteInSmallBmlLine = 0;
                            byteInSmallBmlLine < bytesInLineOfSmallBmp;
                            ++byteInSmallBmlLine)
                        {
                            if (*bigBmpPointer + byteDeviation < *smallBmpPointer ||
                                *bigBmpPointer - byteDeviation > *smallBmpPointer)
                            {
                                templateFound = false;
                                break;
                            }
                            ++bigBmpPointer;
                            ++smallBmpPointer;
                        }
                        if (templateFound)
                        {
                            smallBmpPointer = numPtr4 + smallStride*(1 + heightIndex);
                            bigBmpPointer = numPtr3 + bigStride*(1 + heightIndex);
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