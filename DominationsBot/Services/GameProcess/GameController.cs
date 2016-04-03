using System.Diagnostics;
using System.Drawing;
using DominationsBot.Services.System;
using System.Threading;

namespace DominationsBot.Services.GameProcess
{
    public class GameController
    {
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly MouseController _mouseController;
        private readonly KeyboardController _keyboardController;

        public GameController(EmulatorWindowController emulatorWindowController, MouseController mouseController, KeyboardController keyboardController)
        {
            _emulatorWindowController = emulatorWindowController;
            _mouseController = mouseController;
            _keyboardController = keyboardController;
        }



        public void Unzoom()
        {
            Trace.TraceInformation("Анзумим");
            _emulatorWindowController.Activate();
                //_emulatorWindowController.SendVirtualKey(KeyboardController.VirtualKeys.VK_DOWN);
                _emulatorWindowController.SwipeOffset(WindowStaticPositions.ZoomingButton,new Point(0,250));
                //_emulatorWindowController.MouseCenter();
                //_keyboardController.DownCtrl();
                //Thread.Sleep(0);
                //_mouseController.MouseScrollDown();
                //_keyboardController.UpCtrl();

                Thread.Sleep(500);
        }
    }
}
