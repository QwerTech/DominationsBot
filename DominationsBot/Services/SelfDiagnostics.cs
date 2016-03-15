using System;

namespace DominationsBot.Services
{
    public class SelfDiagnostics
    {
        private readonly BlueStackController _blueStackController;

        public SelfDiagnostics(BlueStackController blueStackController)
        {
            _blueStackController = blueStackController;
        }

        public void Check()
        {
            if (!_blueStackController.IsBlueStacksFound)
                throw new ApplicationException("Не удалось найти окно с BlueStack");
            if (!_blueStackController.IsRunning)
                throw new ApplicationException("BlueStack не запущен");
            if (!_blueStackController.IsRunningWithRequiredDimensions)
            {
                _blueStackController.SetDimensionsIntoRegistry();
                throw new ApplicationException("Неправильное разрешение BlueStack. Сделаны нужные правки в реестре. Перезапустите BlueStack.");
            }
        }
    }
}
