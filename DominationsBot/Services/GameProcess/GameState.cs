using System;
using DominationsBot.Services.ImageProcessing.TemplateFinders;

namespace DominationsBot.Services.GameProcess
{
    public class GameState
    {
        private readonly SaeedTemplateFinder _saeedTemplateFinderProvider;
        private readonly IScreenCapture _screenCapture;
        public int Gold { get; set; }
        public int Food { get; set; }
        public GameState(IScreenCapture screenCapture,
            Func<double, SaeedTemplateFinder> saeedTemplateFinderProvider)
        {
            _screenCapture = screenCapture;
            _saeedTemplateFinderProvider = saeedTemplateFinderProvider(0);
        }

        public bool ShouldCheckAntiSleep { get; } = true;

        public bool IsGameOnMainWindow()
        {
            var snapShot = _screenCapture.SnapShot();

            return !_saeedTemplateFinderProvider.Single(snapShot, BotResources.BmpStoreButton) ||
                   !_saeedTemplateFinderProvider.Single(snapShot, BotResources.BmpBattleButton);
        }
    }
}