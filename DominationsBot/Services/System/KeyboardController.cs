using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace DominationsBot.Services.System
{
    public class KeyboardController
    {
        private const uint WmKeydown = 0x0100;
        private const uint WmKeyup = 0x0101;
        private const uint WmChar = 0x0102;

        public enum VirtualKeys : short
        {
            VkLbutton = 0x01,
            VkRbutton = 0x02,
            VkCancel = 0x03,
            VkMbutton = 0x04,
            //
            VkXbutton1 = 0x05,
            VkXbutton2 = 0x06,
            //
            VkBack = 0x08,
            VkTab = 0x09,
            //
            VkClear = 0x0C,
            VkReturn = 0x0D,
            //
            VkShift = 0x10,
            VkControl = 0x11,
            VkMenu = 0x12,
            VkPause = 0x13,
            VkCapital = 0x14,
            //
            VkKana = 0x15,
            VkHangeul = 0x15, /* old name - should be here for compatibility */
            VkHangul = 0x15,
            VkJunja = 0x17,
            VkFinal = 0x18,
            VkHanja = 0x19,
            VkKanji = 0x19,
            //
            VkEscape = 0x1B,
            //
            VkConvert = 0x1C,
            VkNonconvert = 0x1D,
            VkAccept = 0x1E,
            VkModechange = 0x1F,
            //
            VkSpace = 0x20,
            VkPrior = 0x21,
            VkNext = 0x22,
            VkEnd = 0x23,
            VkHome = 0x24,
            VkLeft = 0x25,
            VkUp = 0x26,
            VkRight = 0x27,
            VkDown = 0x28,
            VkSelect = 0x29,
            VkPrint = 0x2A,
            VkExecute = 0x2B,
            VkSnapshot = 0x2C,
            VkInsert = 0x2D,
            VkDelete = 0x2E,
            VkHelp = 0x2F,
            //
            VkLwin = 0x5B,
            VkRwin = 0x5C,
            VkApps = 0x5D,
            //
            VkSleep = 0x5F,
            //
            VkNumpad0 = 0x60,
            VkNumpad1 = 0x61,
            VkNumpad2 = 0x62,
            VkNumpad3 = 0x63,
            VkNumpad4 = 0x64,
            VkNumpad5 = 0x65,
            VkNumpad6 = 0x66,
            VkNumpad7 = 0x67,
            VkNumpad8 = 0x68,
            VkNumpad9 = 0x69,
            VkMultiply = 0x6A,
            VkAdd = 0x6B,
            VkSeparator = 0x6C,
            VkSubtract = 0x6D,
            VkDecimal = 0x6E,
            VkDivide = 0x6F,
            VkF1 = 0x70,
            VkF2 = 0x71,
            VkF3 = 0x72,
            VkF4 = 0x73,
            VkF5 = 0x74,
            VkF6 = 0x75,
            VkF7 = 0x76,
            VkF8 = 0x77,
            VkF9 = 0x78,
            VkF10 = 0x79,
            VkF11 = 0x7A,
            VkF12 = 0x7B,
            VkF13 = 0x7C,
            VkF14 = 0x7D,
            VkF15 = 0x7E,
            VkF16 = 0x7F,
            VkF17 = 0x80,
            VkF18 = 0x81,
            VkF19 = 0x82,
            VkF20 = 0x83,
            VkF21 = 0x84,
            VkF22 = 0x85,
            VkF23 = 0x86,
            VkF24 = 0x87,
            //
            VkNumlock = 0x90,
            VkScroll = 0x91,
            //
            VkOemNecEqual = 0x92, // '=' key on numpad
            //
            VkOemFjJisho = 0x92, // 'Dictionary' key
            VkOemFjMasshou = 0x93, // 'Unregister word' key
            VkOemFjTouroku = 0x94, // 'Register word' key
            VkOemFjLoya = 0x95, // 'Left OYAYUBI' key
            VkOemFjRoya = 0x96, // 'Right OYAYUBI' key
            //
            VkLshift = 0xA0,
            VkRshift = 0xA1,
            VkLcontrol = 0xA2,
            VkRcontrol = 0xA3,
            VkLmenu = 0xA4,
            VkRmenu = 0xA5,
            //
            VkBrowserBack = 0xA6,
            VkBrowserForward = 0xA7,
            VkBrowserRefresh = 0xA8,
            VkBrowserStop = 0xA9,
            VkBrowserSearch = 0xAA,
            VkBrowserFavorites = 0xAB,
            VkBrowserHome = 0xAC,
            //
            VkVolumeMute = 0xAD,
            VkVolumeDown = 0xAE,
            VkVolumeUp = 0xAF,
            VkMediaNextTrack = 0xB0,
            VkMediaPrevTrack = 0xB1,
            VkMediaStop = 0xB2,
            VkMediaPlayPause = 0xB3,
            VkLaunchMail = 0xB4,
            VkLaunchMediaSelect = 0xB5,
            VkLaunchApp1 = 0xB6,
            VkLaunchApp2 = 0xB7,
            //
            VkOem1 = 0xBA, // ';:' for US
            VkOemPlus = 0xBB, // '+' any country
            VkOemComma = 0xBC, // ',' any country
            VkOemMinus = 0xBD, // '-' any country
            VkOemPeriod = 0xBE, // '.' any country
            VkOem2 = 0xBF, // '/?' for US
            VkOem3 = 0xC0, // '`~' for US
            //
            VkOem4 = 0xDB, //  '[{' for US
            VkOem5 = 0xDC, //  '\|' for US
            VkOem6 = 0xDD, //  ']}' for US
            VkOem7 = 0xDE, //  ''"' for US
            VkOem8 = 0xDF,
            //
            VkOemAx = 0xE1, //  'AX' key on Japanese AX kbd
            VkOem102 = 0xE2, //  "<>" or "\|" on RT 102-key kbd.
            VkIcoHelp = 0xE3, //  Help key on ICO
            VkIco00 = 0xE4, //  00 key on ICO
            //
            VkProcesskey = 0xE5,
            //
            VkIcoClear = 0xE6,
            //
            VkPacket = 0xE7,
            //
            VkOemReset = 0xE9,
            VkOemJump = 0xEA,
            VkOemPa1 = 0xEB,
            VkOemPa2 = 0xEC,
            VkOemPa3 = 0xED,
            VkOemWsctrl = 0xEE,
            VkOemCusel = 0xEF,
            VkOemAttn = 0xF0,
            VkOemFinish = 0xF1,
            VkOemCopy = 0xF2,
            VkOemAuto = 0xF3,
            VkOemEnlw = 0xF4,
            VkOemBacktab = 0xF5,
            //
            VkAttn = 0xF6,
            VkCrsel = 0xF7,
            VkExsel = 0xF8,
            VkEreof = 0xF9,
            VkPlay = 0xFA,
            VkZoom = 0xFB,
            VkNoname = 0xFC,
            VkPa1 = 0xFD,
            VkOemClear = 0xFE
        }
        private readonly IDictionary<VirtualKeys, string> _convertFromVirtualKeysToSendKeyCodes = new Dictionary<VirtualKeys, string>()
        {
            {VirtualKeys.VkDown,  "{DOWN}"},
            {VirtualKeys.VkUp,  "{UP}"}
        };

        private readonly IInputSimulator _inputSimulator;

        public KeyboardController(IInputSimulator inputSimulator)
        {
            _inputSimulator = inputSimulator;
        }

        private static bool AdvancedMode { get; set; }

        public void Send(IntPtr hWnd, string message)
        {
            foreach (int letter in message)
            {
                if (AdvancedMode)
                {
                    VirtualKeys vk = (VirtualKeys)User32.VkKeyScan((char)letter);
                    SendVirtualKey(hWnd, vk);
                }
                else
                    User32.PostMessage(hWnd, WmChar, (IntPtr)letter, IntPtr.Zero);
            }
        }

        public void DownCtrl()
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
        }
        public void UpCtrl()
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
        }
        public void SendVirtualKeyDotNet(VirtualKeys vk)
        {
            SendKeys.SendWait(_convertFromVirtualKeysToSendKeyCodes[vk]);
        }
        public void SendVirtualKey(IntPtr hWnd, VirtualKeys vk)
        {
            IntPtr wParam = (IntPtr)(((short)vk) & 0xFF);
            IntPtr lParam = (IntPtr)1;
            lParam += (int)(User32.MapVirtualKey((uint)wParam, User32.MapvkVkToVsc) << 16);
            bool shift = ((int)vk & 0x0100) == 0x0100 ? true : false;
            if (shift) User32.PostMessage(hWnd, WmKeydown, (IntPtr)VirtualKeys.VkLshift, (IntPtr)0);
            if (!User32.PostMessage(hWnd, WmKeydown, wParam, lParam))
                throw new ApplicationException("Не удалось отправить сообщение");
            Thread.Sleep(5);
            lParam += 1 << 30;
            lParam += 1 << 31;
            if (!User32.PostMessage(hWnd, WmKeyup, wParam, lParam))
                throw new ApplicationException("Не удалось отправить сообщение");
            if (shift) User32.PostMessage(hWnd, WmKeyup, (IntPtr)VirtualKeys.VkLshift, (IntPtr)0);
            Thread.Sleep(5);
        }
    }
}