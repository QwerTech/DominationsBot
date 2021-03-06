﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TemplateFinders;

namespace DominationsBot.Services.GameProcess
{
    public abstract class ObstacleResolver
    {
        public abstract void Act();


        public abstract bool Check();

        public virtual void CheckAndAct()
        {
            if (Check())
            {
                Act();
            }
        }
    }

    public class AntiSleepGame : ObstacleResolver, IWorker
    {
        private readonly SaeedTemplateFinder _buttosFinder;
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly PictureTester _pictureTester;
        private readonly IScreenCapture _screenCapture;
        private readonly ITemplateFinder _sleepScreenFinder;
        private Bitmap _snapShot;

        public AntiSleepGame(Func<double, SaeedTemplateFinder> saeedTemplateFinderProvider, IScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController, PictureTester pictureTester)
        {
            _sleepScreenFinder = saeedTemplateFinderProvider(0);
            _buttosFinder = saeedTemplateFinderProvider(0.3);
            _screenCapture = screenCapture;
            _emulatorWindowController = emulatorWindowController;
            _pictureTester = pictureTester;
        }

        public void DoWork()
        {
            Trace.TraceInformation("Проверяем игру на сон");
            CheckAndAct();
        }


        public override void Act()
        {
            _emulatorWindowController.Click(WindowStaticPositions.SleepingDialog.SleepReloadGame);
            Thread.Sleep(10000);
        }

        public override bool Check()
        {
            return IsGameSleeps();
        }


        public bool IsGameSleeps()
        {
            _snapShot = _screenCapture.SnapShot();
            return _pictureTester.TestLine(_snapShot, WindowStaticPositions.SleepingDialog.CheckDialogLine,
                WindowStaticPositions.SleepingDialog.DialogBackground);
            //var isGameSleeps = _sleepScreenFinder.Exists(_snapShot, Screens.SleepDialog);
            //return isGameSleeps;
        }
    }
}