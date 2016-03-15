using System.Threading;
using DominationsBot.Services.System;

namespace DominationsBot.Services.GameProcess
{
    public class GameController
    {
        private readonly BlueStackController _blueStackController;

        public GameController(BlueStackController blueStackController)
        {
            _blueStackController = blueStackController;
        }

        public void Unzoom()
        {
            for (int i = 0; i < 5; i++)
            {
                _blueStackController.SendVirtualKey(KeyboardController.VirtualKeys.VK_DOWN);
                Thread.Sleep(500);
            }
        }
    }
}
