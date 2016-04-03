using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AForge.Imaging;
using DominationsBot.Extensions;

namespace DominationsBot.Services.ImageProcessing.TemplateFinders
{
    public class EqualTemplateFinder : ITemplateFinder
    {
        private readonly Settings _settings;

        public EqualTemplateFinder(Settings settings)
        {
            _settings = settings;
        }

        public IEnumerable<TemplateMatch> FindTemplate(Bitmap bmp, Bitmap template)
        {
            var search =
                SearchImage.GetSubPositions(bmp, template)
                    .Select(p => new TemplateMatch(new Rectangle(p, template.Size), 1)).ToList();
            bmp.ViewContains(search)
                .Save(Path.Combine(_settings.LogsPath,
                    $"logs/{DateTime.Now:yyyy-dd-M--HH-mm-ss}_EqualTemplateFinderMatches.png"));
            return search;
        }

        public bool Exists(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Any();
        }

        public bool Single(Bitmap bmp, Bitmap template)
        {
            return FindTemplate(bmp, template).Count() == 1;
        }
    }
}