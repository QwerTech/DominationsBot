using DominationsBot.Services.System;
using System.Threading;

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
            _blueStackController.ActivateBlueStack();
            for (int i = 0; i < 30; i++)
            {
                _blueStackController.SendVirtualKey(KeyboardController.VirtualKeys.VK_DOWN);
                Thread.Sleep(500);
            }
        }
    }
}
