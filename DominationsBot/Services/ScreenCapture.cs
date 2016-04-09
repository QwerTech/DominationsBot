﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using DominationsBot.Services.Logging;
using DominationsBot.Services.System;

//using System.Windows.Forms;

namespace DominationsBot.Services
{
    public class ScreenCapture : IScreenCapture
    {

        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly ImageLogger _imageLogger;

        public ScreenCapture(EmulatorWindowController emulatorWindowController,
            ImageLogger imageLogger)
        {
            _emulatorWindowController = emulatorWindowController;
            _imageLogger = imageLogger;
        }

        //private Bitmap SnapShotInternal(Rectangle rect)
        //{
        //    return SnapShot(rect.Left, rect.Top, rect.Width, rect.Height);
        //}


        ///// <summary>
        /////     This capture algorithm should behave as on the background mode of Autoit Coc Bot (as at end of january 2015)
        ///// </summary>
        ///// <param name="left"></param>
        ///// <param name="top"></param>
        ///// <param name="width"></param>
        ///// <param name="height"></param>
        ///// <returns></returns>
        //private Bitmap SnapShot(int left, int top, int width, int height)
        //{
        //    //_emulatorWindowController.Activate();
        //    Bitmap bitMap = null;
        //    var hWnd = _emulatorWindowController.Handle;
        //    var hCaptureDC = Win32.GetWindowDC(hWnd);
        //    var hMemDC = Win32.CreateCompatibleDC(hCaptureDC);
        //    var hBitmap = Win32.CreateCompatibleBitmap(hCaptureDC, width, height);
        //    var hObjOld = Win32.SelectObject(hMemDC, hBitmap);
        //    Win32.BitBlt(hMemDC, 0, 0, width, height, hCaptureDC, left, top, Win32.TernaryRasterOperations.SRCCOPY);
        //    bitMap = Image.FromHbitmap(hBitmap);
        //    Win32.DeleteDC(hMemDC);
        //    Win32.SelectObject(hMemDC, hObjOld);
        //    Win32.ReleaseDC(hWnd, hCaptureDC);
        //    Win32.DeleteObject(hBitmap);
        //    if (bitMap == null)
        //        throw new ApplicationException("Не удалось сделать скриншот");
        //    return bitMap.Clone(new Rectangle(Point.Empty, bitMap.Size), PixelFormat.Format24bppRgb);
        //}

        //private Bitmap SnapShot(int left, int top, int width, int height)
        //{
        //    _emulatorWindowController.Activate();
        //    Thread.Sleep(100);
        //    Bitmap bmp = new Bitmap(width, height);
        //    Graphics memoryGraphics = Graphics.FromImage(bmp);
        //    IntPtr dc = memoryGraphics.GetHdc();
        //    bool success = Win32.PrintWindow(_emulatorWindowController.Handle, dc, 0);
        //    memoryGraphics.ReleaseHdc(dc);
        //    return bmp;
        //}

        public virtual Bitmap SnapShot()
        {
            Rectangle rect;
            rect = _emulatorWindowController.GetLocation();
            var bitMap = SnapShotInternal(rect);
            return bitMap;
        }

        public virtual Bitmap SnapShot(Rectangle rect)
        {
            var emulatorLocation = _emulatorWindowController.GetLocation();
            rect.Offset(emulatorLocation.Location);
            var bitMap = SnapShotInternal(rect);
            return bitMap;
        }
        private Bitmap SnapShotInternal(Rectangle rect)
        {
            Trace.TraceInformation("Делаем скриншот");
            if(!_emulatorWindowController.IsForeground)
                _emulatorWindowController.Activate();
            //Create a new bitmap.
            var bitMap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);

            // Create a graphics object from the bitmap.
            using (var gfxScreenshot = Graphics.FromImage(bitMap))
            {
                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            }
            Trace.TraceInformation("Сделали скриншот");
            _imageLogger.Log(bitMap,"snapshot");
            return bitMap;
        }
    }
}