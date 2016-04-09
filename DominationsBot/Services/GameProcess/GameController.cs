using System.Diagnostics;
using System.Drawing;
using DominationsBot.Services.System;
using System.Threading;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TextReading;

namespace DominationsBot.Services.GameProcess
{
    public class GameController
    {
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly MouseController _mouseController;
        private readonly KeyboardController _keyboardController;
        private readonly ScreenCapture _screenCapture;
        private readonly NumberReader _numberReader;

        public GameController(EmulatorWindowController emulatorWindowController, MouseController mouseController, KeyboardController keyboardController,
            ScreenCapture screenCapture, NumberReader numberReader)
        {
            _emulatorWindowController = emulatorWindowController;
            _mouseController = mouseController;
            _keyboardController = keyboardController;
            _screenCapture = screenCapture;
            _numberReader = numberReader;
        }

        public int ReadGold()
        {
            return ReadStateNumbers(WindowStaticPositions.MainScreen.GoldNumbers, NumberResourcesType.Gold);
        }
        public int ReadFood()
        {
            return ReadStateNumbers(WindowStaticPositions.MainScreen.FoodNumbers, NumberResourcesType.Food);
        }
        private int ReadStateNumbers(Rectangle position, NumberResourcesType resourcesType)
        {
            var snapShot = _screenCapture.SnapShot(position);
            return _numberReader.Read(snapShot, resourcesType);
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
