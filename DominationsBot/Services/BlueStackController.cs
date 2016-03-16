using DominationsBot.Services.System;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace DominationsBot.Services
{
    public class BlueStackController
    {
        private readonly KeyboardController _keyboardController;
        private readonly MouseController _mouseController;

        public BlueStackController(KeyboardController keyboardController, MouseController mouseController)
        {
            _keyboardController = keyboardController;
            _mouseController = mouseController;
        }

        private IntPtr _bshandle = IntPtr.Zero;

        public bool IsBlueStacksFound => Handle != IntPtr.Zero;

        private IntPtr GetBlueStackWindowHandle()
        {
            if (_bshandle != IntPtr.Zero)
                return _bshandle;
            if (_bshandle == IntPtr.Zero)
                _bshandle = Win32.FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player"); // First try
            if (_bshandle == IntPtr.Zero)
                _bshandle = Win32.FindWindow(null, "BlueStacks App Player"); // Maybe the class name has changes
            if (_bshandle == IntPtr.Zero)
            {
                Process[] proc = Process.GetProcessesByName("BlueStacks App Player"); // If failed, then try with .NET functions
                if (!proc.Any())
                    return IntPtr.Zero;
                _bshandle = proc[0].MainWindowHandle;
            }
            if (_bshandle == IntPtr.Zero)
                throw new ApplicationException("Не удалось найти BlueStack окно.");
            return _bshandle;
        }

        public IntPtr Handle => GetBlueStackWindowHandle();

        public void SetDimensionsIntoRegistry()
        {
            bool value = false;

            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\BlueStacks\Guests\Android\FrameBuffer\0", true);
            if (key == null)
                throw new ApplicationException("Не удалось получить доступ к реестру.");
            Registry.SetValue(key.Name, "WindowWidth", 0x00000500, RegistryValueKind.DWord);
            Registry.SetValue(key.Name, "WindowHeight", 0x000002D0, RegistryValueKind.DWord);
            Registry.SetValue(key.Name, "GuestWidth", 0x00000500, RegistryValueKind.DWord);
            Registry.SetValue(key.Name, "GuestHeight", 0x000002D0, RegistryValueKind.DWord);

            Registry.SetValue(key.Name, "Depth", 0x00000010, RegistryValueKind.DWord);
            Registry.SetValue(key.Name, "FullScreen", 0x00000000, RegistryValueKind.DWord);
            Registry.SetValue(key.Name, "WindowState", 0x00000001, RegistryValueKind.DWord);
            Registry.SetValue(key.Name, "HideBootProgress", 0x00000001, RegistryValueKind.DWord);

            key.Close();
        }

        public void Click(int x, int y)
        {
            Click(new Win32.Point(x, y));
        }

        public void Click(Win32.Point point)
        {
            _mouseController.ClickOnPoint(_bshandle, point);
            Thread.Sleep(250);
        }

        public void SendVirtualKey(KeyboardController.VirtualKeys vk)
        {
            _keyboardController.SendVirtualKey(_bshandle, vk);
        }

        public void Send(string message)
        {
            _keyboardController.Send(_bshandle, message);
        }


        // SendMessage and PostMessage should work on hidden forms, use them with the WM_MOUSEXXXX codes and provide the mouse location in the wp or lp parameter, I forget which.
        public bool ClickOnPoint2(IntPtr wndHandle, Point clientPoint, int times = 1, int delay = 0)
        {
            ActivateBlueStack();
            try
            {
                /// set cursor on coords, and press mouse
                if (wndHandle != IntPtr.Zero)
                {
                    for (int x = 0; x < times; x++)
                    {
                        _mouseController.PostMessageSafe(wndHandle, Win32.WM_LBUTTONDOWN, (IntPtr)0x01, (IntPtr)((clientPoint.X) | ((clientPoint.Y) << 16)));
                        _mouseController.PostMessageSafe(wndHandle, Win32.WM_LBUTTONUP, (IntPtr)0x01, (IntPtr)((clientPoint.X) | ((clientPoint.Y) << 16)));
                        Thread.Sleep(delay);
                    }
                }
            }
            catch (global::System.ComponentModel.Win32Exception ex)
            {
                Debug.Assert(false, ex.Message);
                return false;
            }
            return true;
        }

        #region Properties

        /// <summary>
        /// Gets a value indicating whether BlueStacks is running.
        /// </summary>
        /// <value><c>true</c> if BlueStacks is running; otherwise, <c>false</c>.</value>
        public bool IsRunning
        {
            get
            {
                _bshandle = IntPtr.Zero;
                return GetBlueStackWindowHandle() != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Gets a value indicating whether BlueStacks is running with required dimensions.
        /// </summary>
        /// <value><c>true</c> if this BlueStacks is running with required dimensions; otherwise, <c>false</c>.</value>
        public bool IsRunningWithRequiredDimensions
        {
            get
            {
                var rct = new Win32.RECT();
                Win32.GetClientRect(_bshandle, out rct);

                var width = rct.Right - rct.Left; // in Win32 Rect, right and bottom are considered as excluded from the rect. 
                var height = rct.Bottom - rct.Top;

                return (width == 1280) && (height == 720);
            }
        }

        #endregion

        /// <summary>
        /// Activates and displays the window. If the window is 
        /// minimized or maximized, the system restores it to its original size 
        /// and position. An application should use this when restoring 
        /// a minimized window.
        /// </summary>
        /// <returns></returns>
        public bool RestoreBlueStack()
        {
            if (!IsRunning) return false;
            return Win32.ShowWindow(_bshandle, Win32.WindowShowStyle.Restore);
        }

        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        /// <returns></returns>
        public bool ActivateBlueStack()
        {
            if (!IsRunning) return false;
            return Win32.ShowWindow(_bshandle, Win32.WindowShowStyle.Show);
        }
    }
}
