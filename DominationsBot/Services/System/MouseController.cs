using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;

//using System.Windows.Forms; // this is WPF, we don't need WinForms here

namespace DominationsBot.Services.System
{
    public class MouseController
    {
        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Win32.Point lpPoint);

        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

#pragma warning disable 649
        public struct INPUT
        {
            public UInt32 Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        public struct MOUSEINPUT
        {
            public Int32 X;
            public Int32 Y;
            public UInt32 MouseData;
            public UInt32 Flags;
            public UInt32 Time;
            public IntPtr ExtraInfo;
        }

#pragma warning restore 649

        // Mephobia HF reported that this function fails to send mouse clicks to hidden windows 
        public void ClickOnPoint(IntPtr wndHandle, Win32.Point clientPoint)
        {
            var oldPos = Cursor.Position;

            /// get screen coordinates
            ClientToScreen(wndHandle, ref clientPoint);

            /// set cursor on coords, and press mouse
            Cursor.Position = new Point(clientPoint.X, clientPoint.Y);

            var inputMouseDown = new INPUT();
            inputMouseDown.Type = 0; /// input type mouse
            inputMouseDown.Data.Mouse.Flags = 0x0002; /// left button down

            var inputMouseUp = new INPUT();
            inputMouseUp.Type = 0; /// input type mouse
            inputMouseUp.Data.Mouse.Flags = 0x0004; /// left button up

            var inputs = new INPUT[] { inputMouseDown, inputMouseUp };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));

            /// return mouse 
            Cursor.Position = oldPos;

        }

        public void ClickOnPoint2(IntPtr wndHandle, Win32.Point clientPoint)
        {
            ClientToScreen(wndHandle, ref clientPoint);
            var mouseSimulator = new InputSimulator().Mouse;
            mouseSimulator.MoveMouseToPositionOnVirtualDesktop(clientPoint.X, clientPoint.Y);
            mouseSimulator.LeftButtonClick();
        }

        public void PostMessageSafe(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            bool returnValue = Win32.PostMessage(hWnd, msg, wParam, lParam);
            if (!returnValue)
            {
                // An error occured
                throw new global::System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
        }



    }
}
