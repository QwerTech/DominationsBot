using System;
using System.Diagnostics;
using System.Drawing;
using DominationsBot.Services.System;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using DominationsBot.Services.ImageProcessing.TextReading;
using Microsoft.Test.VisualVerification;

namespace DominationsBot.Services.GameProcess
{
    public class GameController
    {
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly MouseController _mouseController;
        private readonly KeyboardController _keyboardController;
        private readonly IScreenCapture _screenCapture;
        private readonly NumberReader _numberReader;
        

        public GameController(EmulatorWindowController emulatorWindowController, MouseController mouseController, KeyboardController keyboardController,
            IScreenCapture screenCapture, NumberReader numberReader, ScreenCapture snapshot)
        {
            _emulatorWindowController = emulatorWindowController;
            _mouseController = mouseController;
            _keyboardController = keyboardController;
            _screenCapture = screenCapture;
            _numberReader = numberReader;
        }

        public int? ReadGold()
        {
            return ReadStateNumbers(WindowStaticPositions.MainScreen.GoldNumbers, NumberResourcesType.Gold);
        }
        public int? ReadFood()
        {
            return ReadStateNumbers(WindowStaticPositions.MainScreen.FoodNumbers, NumberResourcesType.Food);
        }
        private int? ReadStateNumbers(Rectangle position, NumberResourcesType resourcesType)
        {
            var snapShot = _screenCapture.SnapShot(position);
            return _numberReader.Read(snapShot, resourcesType);
        }
    }

    public abstract class WorkabilityDo
    {
        public abstract bool Workability();
        public abstract void Do();

        public void CanWorkAndDo()
        {
            if(!Workability())
                throw new InvalidOperationException();
            Do();
        }
    }
}
