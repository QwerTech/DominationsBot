using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.System;
using System;
using System.Drawing;

//using System.Windows.Forms;

namespace DominationsBot.Services
{

    public class ScreenCapture
    {
        private readonly BitmapPreparer _bitmapPreparer;
        private readonly BlueStackController _blueStackController;

        public ScreenCapture(BitmapPreparer bitmapPreparer, BlueStackController blueStackController)
        {
            _bitmapPreparer = bitmapPreparer;
            _blueStackController = blueStackController;
        }



        // Full client area variant of BackgroundSnapShot
        public Bitmap SnapShot()
        {
            return SnapShot(_blueStackController.GetArea());
        }

        private Bitmap SnapShot(Rectangle rect)
        {
            return SnapShot(rect.Left, rect.Top, rect.Width, rect.Height);
        }


        /// <summary>
        /// This capture algorithm should behave as on the background mode of Autoit Coc Bot (as at end of january 2015)
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Bitmap SnapShot(int left, int top, int width, int height)
        {
            _blueStackController.ActivateBlueStack();
            Bitmap bitMap = null;
            IntPtr hWnd = _blueStackController.Handle;
            IntPtr hCaptureDC = Win32.GetWindowDC(hWnd);
            IntPtr hMemDC = Win32.CreateCompatibleDC(hCaptureDC);
            var hBitmap = Win32.CreateCompatibleBitmap(hCaptureDC, width, height);
            IntPtr hObjOld = Win32.SelectObject(hMemDC, hBitmap);
            Win32.BitBlt(hMemDC, 0, 0, width, height, hCaptureDC, left, top, Win32.TernaryRasterOperations.SRCCOPY);
            bitMap = Bitmap.FromHbitmap(hBitmap);
            Win32.DeleteDC(hMemDC);
            Win32.SelectObject(hMemDC, hObjOld);
            Win32.ReleaseDC(hWnd, hCaptureDC);
            Win32.DeleteObject(hBitmap);
            if (bitMap == null)
                throw new ApplicationException("Не удалось сделать скриншот");
            return bitMap;
        }





        ///// <summary>
        ///// Simple capture implementation using C# high level functions. 
        ///// It should only work when BlueStack is fully visible (NOT background mode). 
        ///// </summary>
        ///// <returns></returns>
        //public Bitmap DotNetSnapShot()
        //{
        //    var rect = _blueStackController.GetLocation();
        //    //Create a new bitmap.
        //    var bitMap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

        //    // Create a graphics object from the bitmap.
        //    using (var gfxScreenshot = Graphics.FromImage(bitMap))
        //    {
        //        // Take the screenshot from the upper left corner to the right bottom corner.
        //        gfxScreenshot.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
        //    }
        //    return bitMap;
        //}
    }
}
