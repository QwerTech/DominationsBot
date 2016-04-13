using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DominationsBot.Services.System
{
    public class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

        [DllImport("psapi.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, uint nSize);

        [DllImport("psapi.dll")]
        public static extern uint GetProcessImageFileName(IntPtr hProcess, StringBuilder lpImageFileName, uint nSize);
    }

    public static class Gdi32
    {
        //Classe che contiene le dichiarazioni API e i tipi

        public enum Bool : int
        {
            @False = 0,

            @True = 1

        }


        public struct Size
        {
            public int Cx;

            public int Cy;


            public Size(int cx, int cy)
            {
                this.Cx = cx;
                this.Cy = cy;
            }
        }

        public struct Blendfunction
        {
            public byte BlendOp;

            public byte BlendFlags;

            public byte SourceConstantAlpha;

            public byte AlphaFormat;

        }

        public const int UlwAlpha = 2;

        public const byte AcSrcOver = 0;

        public const byte AcSrcAlpha = 1;


        /// <summary>
        ///        Creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// <param name="hdc">A handle to an existing DC. If this handle is NULL,
        ///        the function creates a memory DC compatible with the application's current screen.</param>
        /// <returns>
        ///        If the function succeeds, the return value is the handle to a memory DC.
        ///        If the function fails, the return value is <see cref="IntPtr.Zero"/>.
        /// </returns>
        [DllImport("gdi32.dll")]
        public extern static IntPtr CreateCompatibleDC(IntPtr hDc);

        [DllImport("gdi32.dll")]
        public extern static Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public extern static IntPtr SelectObject(IntPtr hDc, IntPtr hObject);

        [DllImport("gdi32.dll")]
        public extern static Bool DeleteObject(IntPtr hObject);


        /// <summary>
        ///        Creates a bitmap compatible with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to a device context.</param>
        /// <param name="nWidth">The bitmap width, in pixels.</param>
        /// <param name="nHeight">The bitmap height, in pixels.</param>
        /// <returns>If the function succeeds, the return value is a handle to the compatible bitmap (DDB). If the function fails, the return value is <see cref="IntPtr.Zero"/>.</returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);


        public enum TernaryRasterOperations : uint
        {
            Srccopy = 0x00CC0020,
            Srcpaint = 0x00EE0086,
            Srcand = 0x008800C6,
            Srcinvert = 0x00660046,
            Srcerase = 0x00440328,
            Notsrccopy = 0x00330008,
            Notsrcerase = 0x001100A6,
            Mergecopy = 0x00C000CA,
            Mergepaint = 0x00BB0226,
            Patcopy = 0x00F00021,
            Patpaint = 0x00FB0A09,
            Patinvert = 0x005A0049,
            Dstinvert = 0x00550009,
            Blackness = 0x00000042,
            Whiteness = 0x00FF0062,
            Captureblt = 0x40000000 //only if WinVer >= 5.0.0 (see wingdi.h)
        }

        /// <summary>
        ///    Performs a bit-block transfer of the color data corresponding to a
        ///    rectangle of pixels from the specified source device context into
        ///    a destination device context.
        /// </summary>
        /// <param name="hdc">Handle to the destination device context.</param>
        /// <param name="nXDest">The leftmost x-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nYDest">The topmost y-coordinate of the destination rectangle (in pixels).</param>
        /// <param name="nWidth">The width of the source and destination rectangles (in pixels).</param>
        /// <param name="nHeight">The height of the source and the destination rectangles (in pixels).</param>
        /// <param name="hdcSrc">Handle to the source device context.</param>
        /// <param name="nXSrc">The leftmost x-coordinate of the source rectangle (in pixels).</param>
        /// <param name="nYSrc">The topmost y-coordinate of the source rectangle (in pixels).</param>
        /// <param name="dwRop">A raster-operation code.</param>
        /// <returns>
        ///    <c>true</c> if the operation succeedes, <c>false</c> otherwise. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(int hdc, int nIndex);

        public const int SwHide = 0;
        public const int SwRestore = 9;
        public const int SwShownormal = 1;
    }
}
