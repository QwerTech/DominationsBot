using AForge.Imaging;
using Castle.Core.Internal;
using DominationsBot.DI;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DominationsBot.Services.ImageProcessing
{
    public class NumberReader
    {
        private readonly ITemplateFinder _templateFinder;
        private readonly ResourceLocator _resourceLocator;

        public NumberReader(ITemplateFinder templateFinder, ResourceLocator resourceLocator)
        {
            _templateFinder = templateFinder;
            _resourceLocator = resourceLocator;
        }

        public int ReadText(Bitmap image)
        {
            List<PointInt> res = new List<PointInt>();

            for (int i = 0; i <= 9; i++)
            {
                var template = _resourceLocator.GetResouce(i);
                var matches = _templateFinder.FindTemplate(image, template);
                var locations = matches.Select(tm => tm.Rectangle.Location);
                res.AddRange(locations.Select(l => new PointInt(l, i)));
            }
            res.Sort(Comparison);


            var result = res.Select((t, i) => t.Value * (res.Count - i)).Sum();
            return result;
        }

        public int Read(Bitmap image)
        {
            List<PointInt> res = new List<PointInt>();

            for (int i = 0; i <= 9; i++)
            {
                var template = _resourceLocator.GetResouce(i);
                var matches = _templateFinder.FindTemplate(image, template);
                var locations = matches.Select(tm => tm.Rectangle.Location);
                res.AddRange(locations.Select(l => new PointInt(l, i)));
            }
            res.Sort(Comparison);


            var result = res.Select((t, i) => t.Value * (res.Count - i)).Sum();
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

    public class TextReader
    {
        private readonly ITemplateFinder _templateFinder;
        private readonly ResourceLocator _resourceLocator;

        public TextReader(ITemplateFinder templateFinder, ResourceLocator resourceLocator)
        {
            _templateFinder = templateFinder;
            _resourceLocator = resourceLocator;
        }

        private class ResourceMatch
        {
            public IEnumerable<TemplateMatch> TemplateMatches { get; set; }
            public KeyValuePair<string, Bitmap> Resource { get; set; }
        }


        public string Read(Bitmap image)
        {
            var resouces = _resourceLocator.GetAllResouces();

            var resourceMatches = resouces
                .Select(
                    pair =>
                    {
                        var filteredMatches = new List<TemplateMatch>();
                        var templateMatches = _templateFinder.FindTemplate(image, pair.Value).ToList();
                        for (int i = 0; i < templateMatches.Count; i++)
                        {
                            var match = templateMatches[i];
                            bool duble = false;
                            for (int j = i + 1; j < templateMatches.Count; j++)
                            {
                                var match2 = templateMatches[j];
                                if (match.Rectangle.IntersectsWith(match2.Rectangle))
                                {
                                    duble = true;
                                    break;
                                }
                            }
                            if (!duble)
                                filteredMatches.Add(match);
                        }
                        return new ResourceMatch { Resource = pair, TemplateMatches = filteredMatches };
                    });

            var flatMathes = resourceMatches.SelectMany(
                resourceMatch =>
                    resourceMatch.TemplateMatches.Select(
                        templateMatch =>
                            new KeyValuePair<string, TemplateMatch>(resourceMatch.Resource.Key, templateMatch)))
                .OrderBy(m => m.Value.Rectangle.Location.X);

            string result = string.Empty;
            flatMathes.ForEach(m => result += m.Key);
            return result;
        }
    }

    public enum NumberResourcesType
    {
        Food,
        Gold,
        Level,
        Citizens,
        Barracks
    }

    public interface ICurrentResourcesType
    {
        NumberResourcesType NumberResourcesType { get; }
    }

    public class ResourcesType : ICurrentResourcesType
    {
        public ResourcesType(NumberResourcesType numberResourcesType)
        {
            NumberResourcesType = numberResourcesType;
        }


        public NumberResourcesType NumberResourcesType { get; }
    }

    public class ResourceLocator
    {
        private readonly ISettings _settings;
        private readonly ResourceNameConverter _converter;

        private readonly IDictionary<NumberResourcesType, string> _resourceFolders = new Dictionary
            <NumberResourcesType, string>()
        {
            {NumberResourcesType.Food, "FoodAndGold"},
            {NumberResourcesType.Barracks, "Barracks"},
            {NumberResourcesType.Gold, "FoodAndGold"},
            {NumberResourcesType.Citizens, "Citizens"},
            {NumberResourcesType.Level, "Level"}
        };

        private readonly string _resourceFolder;

        public ResourceLocator(NumberResourcesType resourcesType, ISettings settings, ResourceNameConverter converter)
        {
            _settings = settings;
            _converter = converter;
            _resourceFolder = _resourceFolders[resourcesType];
        }

        public IDictionary<string, Bitmap> GetAllResouces()
        {
            var pngExtension = ".png";
            var folder = Path.Combine(_settings.SymbolsPath, _resourceFolder);
            var files = Directory.GetFiles(folder, "*" + pngExtension);

            var result =
                files.ToDictionary(
                    s =>
                        _converter.Convert(new FileInfo(s).Name
                            .Replace(folder, string.Empty)
                            .Replace(pngExtension, String.Empty)),
                    s => new Bitmap(s));
            return result;
        }

        public Bitmap GetResouce(int index)
        {
            return (Bitmap)Screens.ResourceManager.GetObject(_resourceFolder + index);
        }
    }

    public class ResourceNameConverter
    {
        private readonly IDictionary<string, string> _resourceFileNameConvertRules = new Dictionary
            <string, string>()
        {
            {"slesh", "/"},
        };

        public string Convert(string input)
        {
            string output;
            if (_resourceFileNameConvertRules.TryGetValue(input, out output))
                return output;
            else
            {
                return input;
            }
        }
    }
}