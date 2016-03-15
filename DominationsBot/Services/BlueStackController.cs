using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using DominationsBot.Services.System;
using Microsoft.Win32;

namespace DominationsBot.Services
{
    public class BlueStackController
    {
        private readonly KeyboardController _keyboardController;
        private readonly MouseController _mouseController;

        private IntPtr bshandle = IntPtr.Zero;

        public BlueStackController(KeyboardController keyboardController, MouseController mouseController)
        {
            _keyboardController = keyboardController;
            _mouseController = mouseController;
        }

        public bool IsBlueStacksFound => bshandle != IntPtr.Zero;

        public IntPtr Handle => GetBlueStackWindowHandle();

        /// <summary>
        ///     Gets a value indicating whether BlueStacks is running.
        /// </summary>
        /// <value><c>true</c> if BlueStacks is running; otherwise, <c>false</c>.</value>
        public bool IsRunning
        {
            get
            {
                bshandle = IntPtr.Zero;
                return GetBlueStackWindowHandle() != IntPtr.Zero;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether BlueStacks is running with required dimensions.
        /// </summary>
        /// <value><c>true</c> if this BlueStacks is running with required dimensions; otherwise, <c>false</c>.</value>
        public bool IsRunningWithRequiredDimensions
        {
            get
            {
                var rectangle = GetArea();
                return (rectangle.Width == 1280) && (rectangle.Height == 720);
            }
        }

        public Rectangle GetArea()
        {
            if (!IsRunning) throw new ApplicationException("blueStack не запущен.");
            Win32.RECT win32rect;
            if (!Win32.GetClientRect(Handle, out win32rect))
                return Rectangle.Empty;

            var area = Rectangle.FromLTRB(win32rect.Left, win32rect.Top, win32rect.Right, win32rect.Bottom);
            if(area==Rectangle.Empty)
                throw new ApplicationException("Не удалось получить размеры окна Bluestack. Скорее всего окно свернуто.");
            return area;
        }

        public Rectangle GetLocation()
        {
            var rectangle = GetArea();
            Win32.Point origin = new Win32.Point(0, 0);
            if (!Win32.ClientToScreen(Handle, ref origin)) throw new ApplicationException("Не удалось получить расположение окна bluestack");
            rectangle.Offset(origin.X, origin.Y);
            return rectangle;
        }

        private IntPtr GetBlueStackWindowHandle(bool force = false)
        {
            if (bshandle == IntPtr.Zero || force)
                bshandle = Win32.FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player");
                    // First try
            if (bshandle == IntPtr.Zero)
                bshandle = Win32.FindWindow(null, "BlueStacks App Player"); // Maybe the class name has changes
            if (bshandle == IntPtr.Zero)
            {
                var proc = Process.GetProcessesByName("BlueStacks App Player");
                    // If failed, then try with .NET functions
                if (!proc.Any())
                    return IntPtr.Zero;
                bshandle = proc[0].MainWindowHandle;
            }
            if (bshandle == IntPtr.Zero)
                throw new ApplicationException("Не удалось найти BlueStack окно.");
            
            return bshandle;
        }

        public void SetDimensionsIntoRegistry()
        {
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
            _mouseController.ClickOnPoint(Handle, point);
            Thread.Sleep(250);
        }

        public void SendVirtualKey(KeyboardController.VirtualKeys vk)
        {
            _keyboardController.SendVirtualKey(Handle, vk);
        }

        public void Send(string message)
        {
            _keyboardController.Send(Handle, message);
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
                    for (var x = 0; x < times; x++)
                    {
                        _mouseController.PostMessageSafe(wndHandle, Win32.WM_LBUTTONDOWN, (IntPtr) 0x01,
                            (IntPtr) (clientPoint.X | (clientPoint.Y << 16)));
                        _mouseController.PostMessageSafe(wndHandle, Win32.WM_LBUTTONUP, (IntPtr) 0x01,
                            (IntPtr) (clientPoint.X | (clientPoint.Y << 16)));
                        Thread.Sleep(delay);
                    }
                }
            }
            catch (Win32Exception ex)
            {
                Debug.Assert(false, ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        ///     Activates and displays the window. If the window is
        ///     minimized or maximized, the system restores it to its original size
        ///     and position. An application should use this when restoring
        ///     a minimized window.
        /// </summary>
        /// <returns></returns>
        public bool RestoreBlueStack()
        {
            if (!IsRunning) return false;
            return Win32.ShowWindow(Handle, Win32.WindowShowStyle.Restore);
        }

        /// <summary>
        ///     Activates the window and displays it in its current size and position.
        /// </summary>
        /// <returns></returns>
        public bool ActivateBlueStack()
        {
            if (!IsRunning) return false;
            return Win32.ShowWindow(Handle, Win32.WindowShowStyle.Show);
        }
    }
}