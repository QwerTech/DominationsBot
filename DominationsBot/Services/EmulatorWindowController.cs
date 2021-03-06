﻿using DominationsBot.Services.System;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using DominationsBot.Extensions;

namespace DominationsBot.Services
{
    public class EmulatorWindowController
    {
        private readonly KeyboardController _keyboardController;
        private readonly MouseController _mouseController;
        private readonly IInputSimulator _inputSimulator;

        protected EmulatorWindowController()
        {
        }

        public EmulatorWindowController(KeyboardController keyboardController, MouseController mouseController, IInputSimulator inputSimulator)
        {
            _keyboardController = keyboardController;
            _mouseController = mouseController;
            _inputSimulator = inputSimulator;
        }

        private IntPtr _handle = IntPtr.Zero;
        public virtual bool IsForeground => IsRunning && IsVisible && User32.GetForegroundWindow() == Handle;

        public virtual IntPtr Handle => GetEmulatorWindowHandle();
        
        public virtual bool IsRunning => Handle != IntPtr.Zero;

        public virtual bool IsVisible => IsRunning && User32.IsWindowVisible(Handle) && !User32.IsIconic(Handle);

        /// <summary>
        ///     Gets a value indicating whether BlueStacks is running with required dimensions.
        /// </summary>
        /// <value><c>true</c> if this BlueStacks is running with required dimensions; otherwise, <c>false</c>.</value>
        public virtual bool IsRunningWithRequiredDimensions
        {
            get
            {
                if (!IsVisible)
                    return false;
                if (User32.IsZoomed(Handle))
                    return false;
                var rectangle = GetArea();
                return (rectangle.Width == 1280 || rectangle.Width == 1282) && (rectangle.Height == 720 || rectangle.Height == 760);
            }
        }

        public virtual Rectangle GetArea()
        {
            if (!IsVisible) Activate();

            User32.Rect win32Rect;
            if (!User32.GetClientRect(Handle, out win32Rect))
                throw new ApplicationException("Не удалось получить размеры окна Эмулятором. Скорее всего окно свернуто.");

            var area = Rectangle.FromLTRB(win32Rect.Left, win32Rect.Top, win32Rect.Right, win32Rect.Bottom);
            if (area == Rectangle.Empty)
                throw new ApplicationException("Не удалось получить размеры окна Эмулятором. Скорее всего окно свернуто.");
            return area;
        }

        public virtual Rectangle GetLocation()
        {
            var rectangle = GetArea();
            User32.Point origin = new User32.Point(0, 0);
            if (!User32.ClientToScreen(Handle, ref origin)) throw new ApplicationException("Не удалось получить расположение окна bluestack");
            rectangle.Offset(origin.X, origin.Y);
            return rectangle;
        }

        private IntPtr GetEmulatorWindowHandle()
        {
            
            if (_handle != IntPtr.Zero)
                return _handle;
            //Trace.TraceInformation("Ищем окно с эмулятором");
            var processes = Process.GetProcesses();
            var process = processes.Single(p => p.ProcessName == "Droid4X");
            _handle = process.MainWindowHandle;
            //if (_handle == IntPtr.Zero)
            //    _handle = Win32.FindWindow(null, "Droid4X 0.9.0 Beta"); // First try
            
            //if (_handle == IntPtr.Zero)
            //    _handle = Win32.FindWindow("WindowsForms10.Window.8.app.0.33c0d9d", "BlueStacks App Player"); // First try
            //if (_handle == IntPtr.Zero)
            //    _handle = Win32.FindWindow(null, "BlueStacks App Player"); // Maybe the class name has changes
            //if (_handle == IntPtr.Zero)
            //{
            //    var proc = Process.GetProcessesByName("BlueStacks App Player");
            //    // If failed, then try with .NET functions
            //    if (!proc.Any())
            //        return IntPtr.Zero;
            //    _handle = proc[0].MainWindowHandle;
            //}
            if (_handle == IntPtr.Zero)
                throw new ApplicationException("Не удалось найти BlueStack окно.");
            return _handle;
        }

        public virtual void SetDimensionsIntoRegistry()
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

        public virtual void Click(int x, int y)
        {
            var clientPoint = new User32.Point(x, y);
            if(!GetArea().Contains(x,y))
                throw new ApplicationException("Запрещается кликать вне окна эмулятора");
            _mouseController.ClickOnPoint2(Handle, clientPoint);
        }

        public virtual void Click(Point point)
        {
            Click(point.X, point.Y);
        }

        public virtual void Swipe(Point start, Point end)
        {
            var windowLocation = GetLocation().Location.ToSize();
            _mouseController.Swipe(start + windowLocation, end + windowLocation);
        }

        public virtual void SwipeOffset(Point start, Point offset)
        {
            var windowLocation = GetLocation().Location.ToSize();
            _mouseController.SwipeOffset(start + windowLocation, offset);
        }

        public virtual void SendVirtualKey(KeyboardController.VirtualKeys vk)
        {
            _keyboardController.SendVirtualKeyDotNet(vk);
        }

        public  virtual void MouseCenter()
        {
            var location = GetLocation();
            var screen = Screen.FromHandle(Handle);
            var windowMiddleX = ushort.MaxValue*((double)location.Middle().X/screen.Bounds.Width);
            var windowMiddleY = ushort.MaxValue *((double)location.Middle().Y / screen.Bounds.Height);

            _inputSimulator.Mouse.MoveMouseTo(windowMiddleX, windowMiddleY);

        }

        public virtual void Send(string message)
        {
            _keyboardController.Send(Handle, message);
        }


        // SendMessage and PostMessage should work on hidden forms, use them with the WM_MOUSEXXXX codes and provide the mouse location in the wp or lp parameter, I forget which.
        public virtual bool ClickOnPoint2(IntPtr wndHandle, Point clientPoint, int times = 1, int delay = 0)
        {
            Activate();
            try
            {
                /// set cursor on coords, and press mouse
                if (wndHandle != IntPtr.Zero)
                {
                    for (var x = 0; x < times; x++)
                    {
                        _mouseController.PostMessageSafe(wndHandle, User32.WmLbuttondown, (IntPtr)0x01,
                            (IntPtr)(clientPoint.X | (clientPoint.Y << 16)));
                        _mouseController.PostMessageSafe(wndHandle, User32.WmLbuttonup, (IntPtr)0x01,
                            (IntPtr)(clientPoint.X | (clientPoint.Y << 16)));
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
        ///     Activates the window and displays it in its current size and position.
        /// </summary>
        /// <returns></returns>
        public virtual void Activate()
        {
            //Trace.TraceInformation("Активируем окно с эмулятором");
            if (IsVisible && IsForeground)
                return;
            if (!User32.ShowWindow(Handle, User32.WindowShowStyle.Restore))
                throw new ApplicationException("Не удалось восстановить окно с Эмулятором");
            if (!User32.ShowWindow(Handle, User32.WindowShowStyle.Show))
                throw new ApplicationException("Не удалось показать окно с Эмулятором");
            
            var foregroundWindow = User32.GetForegroundWindow();
            if (foregroundWindow != Handle)
                if (!User32.SetForegroundWindow(Handle))
                    throw new ApplicationException("Не удалось сделать поставить окно с Эмулятором на передений план");

        }
    }
}
