using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Imaging;
using AForge.Math.Geometry;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using DominationsBot.Services.Logging;

namespace DominationsBot.Services.ImageProcessing.TextReading
{
    public class NumberReader
    {
        private readonly ExhaustiveTemplateMatching _exhaustiveTemplateMatching;
        private readonly ImageLogger _imageLogger;
        private readonly NumbersMainColors _numbersMainColors;
        private readonly PictureTester _pictureTester;
        private readonly Func<NumberResourcesType, ResourceLocator> _resourceLocator;
        private readonly ITemplateFinder _templateFinder;

        public NumberReader(ITemplateFinder templateFinder, Func<NumberResourcesType, ResourceLocator> resourceLocator,
            NumbersMainColors numbersMainColors, PictureTester pictureTester,
            Func<float, ExhaustiveTemplateMatching> finderFunc,
            ImageLogger imageLogger)
        {
            _templateFinder = templateFinder;
            _resourceLocator = resourceLocator;
            _numbersMainColors = numbersMainColors;
            _pictureTester = pictureTester;
            _imageLogger = imageLogger;
            _exhaustiveTemplateMatching = finderFunc(0);
        }



        public int Read(Bitmap image, NumberResourcesType resourcesType)
        {
            var colorFiltering = _numbersMainColors[resourcesType];
            var filteredImage =
                colorFiltering.Apply(
                    image);
            _imageLogger.Log(filteredImage, nameof(filteredImage));
            var numersRectangles = new List<Rectangle>();
            var lastX = 0;
            for (var i = 0; i < filteredImage.Width; i++)
            {
                var lineSegment = new LineSegment(new AForge.Point(i, 0), new AForge.Point(i, filteredImage.Height));
                if (_pictureTester.TestLine(filteredImage, lineSegment, Color.Black))
                {
                    var width = i - lastX;

                    var rectangle = new Rectangle(lastX+1, 0, width + 2, filteredImage.Height);
                    lastX = i;
                    if (width < 4)
                    {
                        continue;
                    }
                    numersRectangles.Add(rectangle);
                }
            }
            var rectangleNumbers = new Dictionary<Rectangle, Dictionary<int, float>>();
            foreach (var numberRectangle in numersRectangles)
            {
                var dictionary = new Dictionary<int, float>();
                if(_pictureTester.TestArea(filteredImage,
                    new Rectangle(numberRectangle.Location,
                        new Size(numberRectangle.Width, ((int) numberRectangle.Height/2))), Color.Black))
                    continue;//comma
                for (var i = 0; i <= 9; i++)
                {
                    using (var template = _resourceLocator(resourcesType).GetResouce(i))
                    {
                        float similarity = 0;
                        if (template.Width > numberRectangle.Width)
                            continue;
                        using (
                            var region =
                                image.Clone(
                                    new Rectangle(numberRectangle.Location,
                                        new Size(Math.Max(template.Width,numberRectangle.Width), numberRectangle.Height)), image.PixelFormat))
                        {
                            similarity =
                                _exhaustiveTemplateMatching.ProcessImage(region,
                                    template).Max(tm => tm.Similarity);
                            //_imageLogger.Log(region,$"region_{i}_{similarity}");
                        }
                        dictionary.Add(i, similarity);
                    }
                }
                if(dictionary.Any())
                rectangleNumbers.Add(numberRectangle, dictionary);
            }
            var result = 0;
            var numberPosition = 1;
            var ordered = rectangleNumbers.OrderBy(r => r.Key.X).Select(k => k.Value);
            foreach (var item in ordered)
            {
                result += item.OrderByDescending(v => v.Value).First().Key*
                          (int) Math.Pow(10, rectangleNumbers.Count - numberPosition);
                numberPosition++;
            }


            return result;
        }

        private static int Comparison(PointInt pos1, PointInt pos2)
        {
            if (pos1.Position.X >= pos2.Position.X)
                return 1;
            return -1;
        }

        private class PointInt
        {
            public readonly int Value;
            public Point Position;

            public PointInt(Point pos, int value)
            {
                Value = value;
                Position = pos;
            }
        }
    }
}