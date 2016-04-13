using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using DominationsBot.Extensions;

//using System.Windows.Forms; // this is WPF, we don't need WinForms here

namespace DominationsBot.Services.System
{
    public class MouseController
    {
        private readonly IInputSimulator _inputSimulator;
        private readonly Func<EmulatorWindowController> _emulatorWindowControllerFunc;

        public MouseController(IInputSimulator inputSimulator, Func<EmulatorWindowController> emulatorWindowControllerFunc)
        {
            _inputSimulator = inputSimulator;
            _emulatorWindowControllerFunc = emulatorWindowControllerFunc;
        }



        public void Swipe(Point start, Point end)
        {
            SwipeOffset(start, end - start.ToSize());
        }

        public void SwipeOffset(Point start, Point offset)
        {
            Cursor.Position = start;
            _inputSimulator.Mouse.LeftButtonDown();

            _inputSimulator.Mouse.MoveMouseBy(offset.X, offset.Y);
            _inputSimulator.Mouse.LeftButtonUp();
        }

        //// Mephobia HF reported that this function fails to send mouse clicks to hidden windows 
        //public void ClickOnPoint(IntPtr wndHandle, Win32.Point clientPoint)
        //{
        //    var oldPos = Cursor.Position;

        //    /// get screen coordinates
        //    ClientToScreen(wndHandle, ref clientPoint);

        //    /// set cursor on coords, and press mouse
        //    Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

        //    var inputMouseDown = new Input {Type = InputType.Mouse};

        //    inputMouseDown.Data.Mouse.dwFlags = Mouseeventf.Leftdown;

        //    var inputMouseUp = new Input {Type = InputType.Mouse};

        //    inputMouseUp.Data.Mouse.dwFlags = Mouseeventf.Leftup;

        //    var inputs = new[] {inputMouseDown, inputMouseUp};
        //    SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof (Input)));

        //    /// return mouse 
        //    Cursor.Position = oldPos;
        //}

        public void ClickOnPoint2(IntPtr wndHandle, User32.Point clientPoint)
        {
            User32.ClientToScreen(wndHandle, ref clientPoint);
            var mouseSimulator = _inputSimulator.Mouse;
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);
            //mouseSimulator.MoveMouseTo(clientPoint.X, clientPoint.Y);
            var emulatorWindowController = _emulatorWindowControllerFunc();
            if (!emulatorWindowController.IsForeground)
                emulatorWindowController.Activate();
            mouseSimulator.LeftButtonClick();
        }

        public void PostMessageSafe(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            var returnValue = User32.PostMessage(hWnd, msg, wParam, lParam);
            if (!returnValue)
            {
                // An error occured
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public void MouseScrollDown()
        {
            var inputMouseDown = new User32.Input {Type = User32.InputType.Mouse};

            inputMouseDown.Data.Mouse.dwFlags = User32.Mouseeventf.Wheel;
            inputMouseDown.Data.Mouse.mouseData = -10*User32.WheelDelta;
            var inputs = new[] {inputMouseDown};
            for (var i = 0; i < 10; i++)
            {
                User32.SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof (User32.Input)));
            }
            //_inputSimulator.Mouse.VerticalScroll(-50);
        }

    }
}