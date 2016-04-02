using System;

namespace DominationsBot.Services
{
    public class SelfDiagnostics
    {
        private readonly EmulatorWindowController _emulatorWindowController;

        public SelfDiagnostics(EmulatorWindowController emulatorWindowController)
        {
            _emulatorWindowController = emulatorWindowController;
        }

        public void Check()
        {
            if (!_emulatorWindowController.IsRunning)
                throw new ApplicationException("BlueStack не запущен");
            if (!_emulatorWindowController.IsVisible)
                throw new ApplicationException("BlueStack не активен.");
            if (!_emulatorWindowController.IsRunningWithRequiredDimensions)
            {
                _emulatorWindowController.SetDimensionsIntoRegistry();
                throw new ApplicationException("Неправильное разрешение BlueStack. Сделаны нужные правки в реестре. Перезапустите BlueStack.");
            }
        }
    }
}
