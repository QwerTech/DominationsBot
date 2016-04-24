using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AForge.Imaging;
using Castle.Core.Internal;
using DominationsBot.Services.ImageProcessing.TemplateFinders;

namespace DominationsBot.Services.ImageProcessing.TextReading
{
    public class TextReader
    {
        private readonly Func<NumberResourcesType, ResourceLocator> _resourceLocator;
        private readonly ITemplateFinder _templateFinder;

        public TextReader(ITemplateFinder templateFinder, Func<NumberResourcesType, ResourceLocator> resourceLocator)
        {
            _templateFinder = templateFinder;
            _resourceLocator = resourceLocator;
        }


        public string Read(Bitmap image, NumberResourcesType resourcesType)
        {
            var resouces = _resourceLocator(resourcesType).GetAllResouces();

            var resourceMatches = resouces
                .Select(
                    pair =>
                    {
                        var filteredMatches = new List<TemplateMatch>();
                        var templateMatches = _templateFinder.FindTemplate(image, pair.Value).ToList();
                        for (var i = 0; i < templateMatches.Count; i++)
                        {
                            var match = templateMatches[i];
                            var duble = false;
                            for (var j = i + 1; j < templateMatches.Count; j++)
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
                        return new ResourceMatch {Resource = pair, TemplateMatches = filteredMatches};
                    });

            var flatMathes = resourceMatches.SelectMany(
                resourceMatch =>
                    resourceMatch.TemplateMatches.Select(
                        templateMatch =>
                            new KeyValuePair<string, TemplateMatch>(resourceMatch.Resource.Key, templateMatch)))
                .OrderBy(m => m.Value.Rectangle.Location.X);

            var result = string.Empty;
            flatMathes.ForEach(m => result += m.Key);
            return result;
        }

        private class ResourceMatch
        {
            public IEnumerable<TemplateMatch> TemplateMatches { get; set; }
            public KeyValuePair<string, Bitmap> Resource { get; set; }
        }
    }

    public enum NumberResourcesType
    {
        Food,
        Gold,
        Level,
        Citizens,
        Barracks,
        BeforeBattleTroops,
        InBattleTroops,
        InSaveBattleTroops
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
        private readonly ResourceNameConverter _converter;

        private readonly IDictionary<NumberResourcesType, string> _resourceFolders = new Dictionary
            <NumberResourcesType, string>
        {
            {NumberResourcesType.Food, "FoodAndGold"},
            {NumberResourcesType.Barracks, "Barracks"},
            {NumberResourcesType.Gold, "FoodAndGold"},
            {NumberResourcesType.Citizens, "Citizens"},
            {NumberResourcesType.Level, "Level"},
            {NumberResourcesType.BeforeBattleTroops, "FoodAndGold"},
        };

        private readonly ISettings _settings;

        private readonly string _resourceFolder;

        private IDictionary<string, Bitmap> _cache;

        public ResourceLocator(ISettings settings, ResourceNameConverter converter, NumberResourcesType resourcesType)
        {
            _resourceFolder = _resourceFolders[resourcesType];
            _settings = settings;
            _converter = converter;
        }

        public IDictionary<string, Bitmap> GetAllResouces()
        {
            if (_cache != null)
                return _cache;
            var pngExtension = ".png";
            var folder = Path.Combine(BotResources.Symbols.ThisPath, _resourceFolder);
            var files = Directory.GetFiles(folder, "*" + pngExtension);

            _cache =
                files.ToDictionary(
                    s =>
                        _converter.Convert(new FileInfo(s).Name
                            .Replace(folder, string.Empty)
                            .Replace(pngExtension, string.Empty)),
                    s => new Bitmap(s));
            return _cache;
        }

        public Bitmap GetResouce(int index)
        {
            return GetAllResouces()[index.ToString()];
        }
    }

    public class ResourceNameConverter
    {
        private readonly IDictionary<string, string> _resourceFileNameConvertRules = new Dictionary
            <string, string>
        {
            {"slesh", "/"}
        };

        public string Convert(string input)
        {
            string output;
            if (_resourceFileNameConvertRules.TryGetValue(input, out output))
                return output;
            return input;
        }
    }
}