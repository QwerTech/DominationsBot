using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DominationsBot.Services.System
{
    public static class ProcessHelper
    {
        #region Interop

        [DllImport("psapi.dll")]
        private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, uint nSize);

        [DllImport("psapi.dll")]
        private static extern uint GetProcessImageFileName(IntPtr hProcess, StringBuilder lpImageFileName, uint nSize);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, StringBuilder lpExeName, ref uint lpdwSize);

        [DllImport("kernel32.dll")]
        private static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, uint uuchMax);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        [Flags]
        private enum ProcessAccessFlags : uint
        {
            Read = 0x10, // PROCESS_VM_READ
            QueryInformation = 0x400 // PROCESS_QUERY_INFORMATION
        }

        #endregion
        private const uint PathBufferSize = 512; // plenty big enough
        private readonly static StringBuilder PathBuffer = new StringBuilder((int)PathBufferSize);


        private static string GetExecutablePath(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero) { return string.Empty; } // not a valid window handle

            // Get the process id
            uint processid;
            GetWindowThreadProcessId(hwnd, out processid);

            // Try the GetModuleFileName method first since it's the fastest. 
            // May return ACCESS_DENIED (due to VM_READ flag) if the process is not owned by the current user.
            // Will fail if we are compiled as x86 and we're trying to open a 64 bit process...not allowed.
            IntPtr hprocess = OpenProcess(ProcessAccessFlags.QueryInformation | ProcessAccessFlags.Read, false, processid);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    if (GetModuleFileNameEx(hprocess, IntPtr.Zero, PathBuffer, PathBufferSize) > 0)
                    {
                        return PathBuffer.ToString();
                    }
                }
                finally
                {
                    CloseHandle(hprocess);
                }
            }

            hprocess = OpenProcess(ProcessAccessFlags.QueryInformation, false, processid);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    // Try this method for Vista or higher operating systems
                    uint size = PathBufferSize;
                    if ((Environment.OSVersion.Version.Major >= 6) &&
                     (QueryFullProcessImageName(hprocess, 0, PathBuffer, ref size) && (size > 0)))
                    {
                        return PathBuffer.ToString();
                    }

                    // Try the GetProcessImageFileName method
                    if (GetProcessImageFileName(hprocess, PathBuffer, PathBufferSize) > 0)
                    {
                        string dospath = PathBuffer.ToString();
                        foreach (string drive in Environment.GetLogicalDrives())
                        {
                            if (QueryDosDevice(drive.TrimEnd('\\'), PathBuffer, PathBufferSize) > 0)
                            {
                                if (dospath.StartsWith(PathBuffer.ToString()))
                                {
                                    return drive + dospath.Remove(0, PathBuffer.Length);
                                }
                            }
                        }
                    }
                }
                finally
                {
                    CloseHandle(hprocess);
                }
            }

            return string.Empty;
        }
        
    }
}
