using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using DominationsBot.Extensions;

//using System.Windows.Forms; // this is WPF, we don't need WinForms here

namespace DominationsBot.Services.System
{
    public class MouseController
    {
        private readonly IInputSimulator _inputSimulator;

        public MouseController(IInputSimulator inputSimulator)
        {
            _inputSimulator = inputSimulator;
        }

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Win32.Point lpPoint);

        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] Input[] pInputs,
            int cbSize);

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

        // Mephobia HF reported that this function fails to send mouse clicks to hidden windows 
        public void ClickOnPoint(IntPtr wndHandle, Win32.Point clientPoint)
        {
            var oldPos = Cursor.Position;

            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseDown = new Input {Type = InputType.Mouse};

            inputMouseDown.Data.Mouse.dwFlags = Mouseeventf.Leftdown;

            var inputMouseUp = new Input {Type = InputType.Mouse};

            inputMouseUp.Data.Mouse.dwFlags = Mouseeventf.Leftup;

            var inputs = new[] {inputMouseDown, inputMouseUp};
            SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof (Input)));

            /// return mouse 
            Cursor.Position = oldPos;
        }

        public void ClickOnPoint2(IntPtr wndHandle, Win32.Point clientPoint)
        {
            ClientToScreen(wndHandle, ref clientPoint);
            var mouseSimulator = _inputSimulator.Mouse;
            mouseSimulator.MoveMouseToPositionOnVirtualDesktop(clientPoint.X, clientPoint.Y);
            mouseSimulator.LeftButtonClick();
        }

        public void PostMessageSafe(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            var returnValue = Win32.PostMessage(hWnd, msg, wParam, lParam);
            if (!returnValue)
            {
                // An error occured
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        public void MouseScrollDown()
        {
            var inputMouseDown = new Input {Type = InputType.Mouse};

            inputMouseDown.Data.Mouse.dwFlags = Mouseeventf.Wheel;
            inputMouseDown.Data.Mouse.mouseData = -10*WheelDelta;
            var inputs = new[] {inputMouseDown};
            for (var i = 0; i < 10; i++)
            {
                SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof (Input)));
            }
            //_inputSimulator.Mouse.VerticalScroll(-50);
        }

#pragma warning disable 649
        [StructLayout(LayoutKind.Sequential)]
        public struct Input
        {
            internal InputType Type;
            internal InputUnion Data;

            internal static int Size
            {
                get { return Marshal.SizeOf(typeof (Input)); }
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
            [FieldOffset(0)] internal Mouseinput Mouse;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Mousekeybdhardwareinput
        {
            [FieldOffset(0)] public Mouseinput Mouse;
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
    }
}