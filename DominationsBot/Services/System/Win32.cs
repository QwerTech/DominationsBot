using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DominationsBot.Services.System
{
    public class Win32
    {
        //Classe che contiene le dichiarazioni API e i tipi
        public enum Bool : int
        {
            @False = 0,

            @True = 1

        }

        public struct Point
        {
            public int X;

            public int Y;


            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public Point(global::System.Drawing.Point point) : this(point.X, point.Y)
            {

            }
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



        [DllImport("user32.dll")]
        public extern static Bool UpdateLayeredWindow(IntPtr handle, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, int crKey, ref Blendfunction pblend, int dwFlags);

        [DllImport("user32.dll")]
        public extern static IntPtr GetDC(IntPtr handle);

        [DllImport("user32.dll", ExactSpelling = true)]
        public extern static int ReleaseDC(IntPtr handle, IntPtr hDc);

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

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        public static string GetClassName(IntPtr hWnd)
        {
            StringBuilder stringBuilder = new StringBuilder(100);
            var windowText = GetClassName(hWnd, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }

        public static string GetWindowText(IntPtr hWnd)
        {
            int capacity = GetWindowTextLength(hWnd) * 2;
            StringBuilder stringBuilder = new StringBuilder(capacity);
            var windowText = GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);

        /// <summary>
        ///        Creates a bitmap compatible with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to a device context.</param>
        /// <param name="nWidth">The bitmap width, in pixels.</param>
        /// <param name="nHeight">The bitmap height, in pixels.</param>
        /// <returns>If the function succeeds, the return value is a handle to the compatible bitmap (DDB). If the function fails, the return value is <see cref="IntPtr.Zero"/>.</returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap([In] IntPtr hdc, int nWidth, int nHeight);

        // To capture only the client area of window, use PW_CLIENTONLY = 0x1 as nFlags
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDc, uint nFlags);
        /// <summary>
        /// Меняет активное окно Windows
        /// </summary>
        /// <param name="hWnd">Дескриптор окна, которое должно стать активным</param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// Получает дескриптор активного окна
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();


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

        [DllImport("User32")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        // The WM_COMMAND message is sent when the user selects a command item from a menu, 
        // when a control sends a notification message to its parent window, or when an 
        // accelerator keystroke is translated.
        public const uint WmCommand = 0x111;
        public const uint WmLbuttondown = 0x201;
        public const uint WmLbuttonup = 0x202;
        public const uint WmLbuttondblclk = 0x203;
        public const uint WmRbuttondown = 0x204;
        public const uint WmRbuttonup = 0x205;
        public const uint WmRbuttondblclk = 0x206;
        public const uint WmKeydown = 0x100;
        public const uint WmKeyup = 0x101;


        // The FindWindow function retrieves a handle to the top-level window whose class name
        // and window name match the specified strings. This function does not search child windows.
        // This function does not perform a case-sensitive search.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        // The FindWindowEx function retrieves a handle to a window whose class name 
        // and window name match the specified strings. The function searches child windows, beginning
        // with the one following the specified child window. This function does not perform a case-sensitive search.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);


        // The SendMessage function sends the specified message to a 
        // window or windows. It calls the window procedure for the specified 
        // window and does not return until the window procedure has processed the message. 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(
          IntPtr hWnd,   // handle to destination window
          UInt32 msg,    // message
          IntPtr wParam, // first message parameter
          IntPtr lParam  // second message parameter 
          );

        //[DllImport("User32.dll")]
        //public static extern Int32 SendMessage(
        //  int hWnd,               // handle to destination window
        //  int Msg,                // message
        //  int wParam,             // first message parameter
        //  [MarshalAs(UnmanagedType.LPStr)] string lParam); // second message parameter

        //[DllImport("User32.dll")]
        //public static extern Int32 SendMessage(
        //  int hWnd,               // handle to destination window
        //  int Msg,                // message
        //  int wParam,             // first message parameter
        //  int lParam);			// second message parameter

        /// <summary>Shows a Window</summary>
        /// <remarks>
        /// <para>To perform certain special effects when showing or hiding a 
        /// window, use AnimateWindow.</para>
        ///<para>The first time an application calls ShowWindow, it should use 
        ///the WinMain function's nCmdShow parameter as its nCmdShow parameter. 
        ///Subsequent calls to ShowWindow must use one of the values in the 
        ///given list, instead of the one specified by the WinMain function's 
        ///nCmdShow parameter.</para>
        ///<para>As noted in the discussion of the nCmdShow parameter, the 
        ///nCmdShow value is ignored in the first call to ShowWindow if the 
        ///program that launched the application specifies startup information 
        ///in the structure. In this case, ShowWindow uses the information 
        ///specified in the STARTUPINFO structure to show the window. On 
        ///subsequent calls, the application must call ShowWindow with nCmdShow 
        ///set to SW_SHOWDEFAULT to use the startup information provided by the 
        ///program that launched the application. This behavior is designed for 
        ///the following situations: </para>
        ///<list type="">
        ///    <item>Applications create their main window by calling CreateWindow 
        ///    with the WS_VISIBLE flag set. </item>
        ///    <item>Applications create their main window by calling CreateWindow 
        ///    with the WS_VISIBLE flag cleared, and later call ShowWindow with the 
        ///    SW_SHOW flag set to make it visible.</item>
        ///</list></remarks>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="nCmdShow">Specifies how the window is to be shown. 
        /// This parameter is ignored the first time an application calls 
        /// ShowWindow, if the program that launched the application provides a 
        /// STARTUPINFO structure. Otherwise, the first time ShowWindow is called, 
        /// the value should be the value obtained by the WinMain function in its 
        /// nCmdShow parameter. In subsequent calls, this parameter can be one of 
        /// the WindowShowStyle members.</param>
        /// <returns>
        /// If the window was previously visible, the return value is nonzero. 
        /// If the window was previously hidden, the return value is zero.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref Windowinfo pwi);
        [StructLayout(LayoutKind.Sequential)]
        public struct Windowinfo
        {
            //cbSize
            //Type: DWORD
            //The size of the structure, in bytes.The caller must set this member to sizeof(WINDOWINFO).
            //rcWindow
            //Type: RECT
            //The coordinates of the window.
            //rcClient
            //Type: RECT
            //The coordinates of the client area.
            //dwStyle
            //Type: DWORD
            //The window styles. For a table of window styles, see Window Styles.
            //dwExStyle
            //Type: DWORD
            //The extended window styles.For a table of extended window styles, see Extended Window Styles.
            //dwWindowStatus
            //Type: DWORD
            //The window status. If this member is WS_ACTIVECAPTION (0x0001), the window is active.Otherwise, this member is zero.
            //cxWindowBorders
            //Type: UINT
            //The width of the window border, in pixels.
            //cyWindowBorders
            //Type: UINT
            //The height of the window border, in pixels.
            //atomWindowType
            //Type: ATOM
            //The window class atom (see RegisterClass).
            //wCreatorVersion
            //Type: WORD
            //The Windows version of the application that created the window.
            public uint cbSize;
            public Rect rcWindow;
            public Rect rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;

            public Windowinfo(Boolean? filler) : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            {
                cbSize = (UInt32)(Marshal.SizeOf(typeof(Windowinfo)));
            }

        }
        /// <summary>
        /// Maximized
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsZoomed(IntPtr hWnd);
        /// <summary>
        /// Minimized
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>Enumeration of the different ways of showing a window using 
        /// ShowWindow</summary>
        public enum WindowShowStyle : uint
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized 
            /// or maximized, the system restores it to its original size and 
            /// position. An application should specify this flag when displaying 
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position. 
            /// This value is similar to "ShowNormal", except the window is not 
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size 
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next 
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is 
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This 
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is 
            /// minimized or maximized, the system restores it to its original size 
            /// and position. An application should specify this flag when restoring 
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
            /// that owns the window is hung. This flag should only be used when 
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

        [DllImport("psapi.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, uint nSize);

        [DllImport("psapi.dll")]
        public static extern uint GetProcessImageFileName(IntPtr hProcess, StringBuilder lpImageFileName, uint nSize);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern short VkKeyScan(char ch);

        public const uint MapvkVkToVsc = 0x00;
        public const uint MapvkVscToVk = 0x01;
        public const uint MapvkVkToChar = 0x02;
        public const uint MapvkVscToVkEx = 0x03;
        public const uint MapvkVkToVscEx = 0x04;

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);
    }

}
