using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DominationsBot.Services.System
{
    public class User32
    {
        [DllImport("User32")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("User32")]
        public static extern bool UpdateWindow(IntPtr hWnd);
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
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

#pragma warning disable 649
        [StructLayout(LayoutKind.Sequential)]
        public struct Input
        {
            internal InputType Type;
            internal InputUnion Data;

            internal static int Size
            {
                get { return Marshal.SizeOf(typeof(Input)); }
            }
        }

        internal enum InputType : uint
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            internal Mouseinput Mouse;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Mousekeybdhardwareinput
        {
            [FieldOffset(0)]
            public Mouseinput Mouse;
        }

        public const int WheelDelta = 120;

        [StructLayout(LayoutKind.Sequential)]
        public struct Mouseinput
        {
            internal int dx;
            internal int dy;
            internal int mouseData;
            internal Mouseeventf dwFlags;
            internal uint time;
            internal UIntPtr dwExtraInfo;
        }

        [Flags]
        internal enum Mouseeventf : uint
        {
            Absolute = 0x8000,
            Hwheel = 0x01000,
            Move = 0x0001,
            MoveNocoalesce = 0x2000,
            Leftdown = 0x0002,
            Leftup = 0x0004,
            Rightdown = 0x0008,
            Rightup = 0x0010,
            Middledown = 0x0020,
            Middleup = 0x0040,
            Virtualdesk = 0x4000,
            Wheel = 0x0800,
            Xdown = 0x0080,
            Xup = 0x0100
        }

#pragma warning restore 649
        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] Input[] pInputs,
            int cbSize);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern short VkKeyScan(char ch);

        public const uint MapvkVkToVsc = 0x00;
        public const uint MapvkVscToVk = 0x01;
        public const uint MapvkVkToChar = 0x02;
        public const uint MapvkVscToVkEx = 0x03;
        public const uint MapvkVkToVscEx = 0x04;

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);
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

        [DllImport("user32.dll")]
        public extern static Gdi32.Bool UpdateLayeredWindow(IntPtr handle, IntPtr hdcDst, ref Point pptDst, ref Gdi32.Size psize, IntPtr hdcSrc, ref Point pprSrc, int crKey, ref Gdi32.Blendfunction pblend, int dwFlags);

        [DllImport("user32.dll")]
        public extern static IntPtr GetDC(IntPtr handle);

        [DllImport("user32.dll", ExactSpelling = true)]
        public extern static int ReleaseDC(IntPtr handle, IntPtr hDc);
    }
}