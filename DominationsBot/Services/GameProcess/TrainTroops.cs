using AForge.Imaging;
using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DominationsBot.Services.ImageProcessing.TextReading;

namespace DominationsBot.Services.GameProcess
{
    public class TrainTroops : IWorker
    {
        private readonly ResizeTemplateFinder _finder;
        private readonly ScreenCapture _screenCapture;
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly ResourceLocator _resourceLocatorProvider;
        private readonly WorkingAreaFilter _workingAreaFilter;

        public TrainTroops(ResizeTemplateFinder finder, ScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController,
            Func<NumberResourcesType, ResourceLocator> resourceLocatorProvider,
            WorkingAreaFilter workingAreaFilter)
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _emulatorWindowController = emulatorWindowController;
            _resourceLocatorProvider = resourceLocatorProvider(NumberResourcesType.Barracks);
            _workingAreaFilter = workingAreaFilter;
        }

        public void DoWork()
        {
            var snapShot = _screenCapture.SnapShot();
            IEnumerable<TemplateMatch> templateMatches = Enumerable.Empty<TemplateMatch>();
            foreach (var resource in _resourceLocatorProvider.GetAllResouces())
            {
                templateMatches =
                    _finder.FindTemplate(snapShot, resource.Value)
                        .Where(tm => _workingAreaFilter.IsInWorkingArea(tm.Rectangle.Middle()))
                        .ToList();
                if (templateMatches.Any())
                    break;
            }

            _emulatorWindowController.Click(templateMatches.FirstOrDefault().Rectangle.Middle());

            Thread.Sleep(250);
            _emulatorWindowController.Click(WindowStaticPositions.Barracks.TrainButton);
            Thread.Sleep(250);
            for (int i = 0; i < 10; i++)
            {
                _emulatorWindowController.Click(WindowStaticPositions.Barracks.TrainSpearmanButton);
                Thread.Sleep(250);
            }
            _emulatorWindowController.Click(WindowStaticPositions.Barracks.CloseButton);
            Thread.Sleep(250);
            Thread.Sleep(1000);
        }
    }
}