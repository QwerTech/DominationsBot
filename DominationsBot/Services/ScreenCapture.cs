using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.System;

//using System.Windows.Forms;

namespace DominationsBot.Services
{
    public class ScreenCapture
    {
        private readonly BitmapPreparer _bitmapPreparer;
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly Settings _settings;

        public ScreenCapture(BitmapPreparer bitmapPreparer, EmulatorWindowController emulatorWindowController,
            Settings settings)
        {
            _bitmapPreparer = bitmapPreparer;
            _emulatorWindowController = emulatorWindowController;
            _settings = settings;
        }


        //// Full client area variant of BackgroundSnapShot
        //public Bitmap SnapShot()
        //{
        //    return SnapShot(_emulatorWindowController.GetArea());
        //}

        //private Bitmap SnapShot(Rectangle rect)
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
        //    _emulatorWindowController.Activate();
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
        //    Bitmap bmp = new Bitmap(width, height);
        //    Graphics memoryGraphics = Graphics.FromImage(bmp);
        //    IntPtr dc = memoryGraphics.GetHdc();
        //    bool success = Win32.PrintWindow(_emulatorWindowController.Handle, dc, 0);
        //    memoryGraphics.ReleaseHdc(dc);
        //    return bmp;
        //}
        /// <summary>
        ///     Simple capture implementation using C# high level functions.
        ///     It should only work when BlueStack is fully visible (NOT background mode).
        /// </summary>
        /// <returns></returns>
        public Bitmap SnapShot()
        {
            Trace.TraceInformation("Делаем скриншот");
            _emulatorWindowController.Activate();
            var rect = _emulatorWindowController.GetLocation();
            //Create a new bitmap.
            var bitMap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            using (var gfxScreenshot = Graphics.FromImage(bitMap))
            {
                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            }
            Trace.TraceInformation("Сделали скриншот");
            
            
            bitMap.Save(Path.Combine(_settings.LogsPath,$"{DateTime.Now:yyyy-dd-M--HH-mm-ss}_snapshot.png"));
            return bitMap;
        }
    }
}