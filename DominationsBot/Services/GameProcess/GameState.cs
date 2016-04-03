using System;
using DominationsBot.Services.ImageProcessing.TemplateFinders;

namespace DominationsBot.Services.GameProcess
{
    public class GameState
    {
        private readonly SaeedTemplateFinder _saeedTemplateFinderProvider;
        private readonly ScreenCapture _screenCapture;

        public GameState(ScreenCapture screenCapture,
            Func<double, SaeedTemplateFinder> saeedTemplateFinderProvider)
        {
            _screenCapture = screenCapture;
            _saeedTemplateFinderProvider = saeedTemplateFinderProvider(0);
        }

        public bool ShouldCheckAntiSleep { get; } = true;

        public bool IsGameOnMainWindow()
        {
            var snapShot = _screenCapture.SnapShot();

            return !_saeedTemplateFinderProvider.Single(snapShot, Screens.StoreButton) ||
                   !_saeedTemplateFinderProvider.Single(snapShot, Screens.BattleButton);
        }
    }
}