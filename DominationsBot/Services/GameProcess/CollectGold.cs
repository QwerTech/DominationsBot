using DominationsBot.Extensions;
using DominationsBot.Tools;

namespace DominationsBot.Services.GameProcess
{
    public class CollectGold : IWorker
    {
        private readonly ResizeTemplateFinder _finder;
        private readonly ScreenCapture _screenCapture;

        public CollectGold(ResizeTemplateFinder finder, ScreenCapture screenCapture)
        {
            _finder = finder;
            _screenCapture = screenCapture;
        }

        public void DoWork()
        {
            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Screens.coin);

            foreach (var match in templateMatches)
            {
                BlueStackHelper.Click(new Win32.Point(match.Rectangle.Multiply(_finder.Divisor).Middle()));
            }
            if (templateMatches.Length != 0)
                DoWork();
        }
    }
}