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
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs,
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

            var inputMouseDown = new INPUT {Type = InputType.MOUSE};

            inputMouseDown.Data.Mouse.dwFlags = MOUSEEVENTF.LEFTDOWN;

            var inputMouseUp = new INPUT {Type = InputType.MOUSE};

            inputMouseUp.Data.Mouse.dwFlags = MOUSEEVENTF.LEFTUP;

            var inputs = new[] {inputMouseDown, inputMouseUp};
            SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof (INPUT)));

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
            var inputMouseDown = new INPUT {Type = InputType.MOUSE};

            inputMouseDown.Data.Mouse.dwFlags = MOUSEEVENTF.WHEEL;
            inputMouseDown.Data.Mouse.mouseData = -10*WHEEL_DELTA;
            var inputs = new[] {inputMouseDown};
            for (var i = 0; i < 10; i++)
            {
                SendInput((uint) inputs.Length, inputs, Marshal.SizeOf(typeof (INPUT)));
            }
            //_inputSimulator.Mouse.VerticalScroll(-50);
        }

#pragma warning disable 649
        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            internal InputType Type;
            internal InputUnion Data;

            internal static int Size
            {
                get { return Marshal.SizeOf(typeof (INPUT)); }
            }
        }

        internal enum InputType : uint
        {
            MOUSE = 0,
            KEYBOARD = 1,
            HARDWARE = 2
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)] internal MOUSEINPUT Mouse;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)] public MOUSEINPUT Mouse;
        }

        public const int WHEEL_DELTA = 120;

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            internal int dx;
            internal int dy;
            internal int mouseData;
            internal MOUSEEVENTF dwFlags;
            internal uint time;
            internal UIntPtr dwExtraInfo;
        }

        [Flags]
        internal enum MOUSEEVENTF : uint
        {
            ABSOLUTE = 0x8000,
            HWHEEL = 0x01000,
            MOVE = 0x0001,
            MOVE_NOCOALESCE = 0x2000,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            VIRTUALDESK = 0x4000,
            WHEEL = 0x0800,
            XDOWN = 0x0080,
            XUP = 0x0100
        }

#pragma warning restore 649
    }
}