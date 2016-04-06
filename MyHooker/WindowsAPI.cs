using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MyHooker
{
    class WindowsAPIDllStuff
    {
        public struct SECURITY_ATTRIBUTES
        {
            public int length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        public struct STARTUPINFO
        {
            public uint cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        public struct MODULEINFO
        {
            public IntPtr lpBaseOfDll;
            public uint SizeOfImage;
            public IntPtr EntryPoint;
        }

        public const int PROCESS_VM_WRITE = 0x0020;
        public const int PROCESS_VM_OPERATION = 0x0008;


        [DllImport("kernel32.dll")]
        public static extern bool CreateProcess(
            string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
            bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
            string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress,
            byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, out MODULEINFO lpmodinfo, uint cb);
    }
    class WindowsAPI
    {
        public static bool CreateProcess(string executablePath)
        {
            WindowsAPIDllStuff.STARTUPINFO si = new WindowsAPIDllStuff.STARTUPINFO();
            WindowsAPIDllStuff.PROCESS_INFORMATION pi = new WindowsAPIDllStuff.PROCESS_INFORMATION();
            return WindowsAPIDllStuff.CreateProcess(executablePath, null, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref si, out pi);
        }

        public static Process GetProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0) {
                throw new Exception("uh oh");
            }
            return processes[0];
        }

        public static IntPtr GetProcessHandle(Process process)
        {
            return WindowsAPIDllStuff.OpenProcess(0x1F0FFF, false, process.Id);
        }

        public static bool ReadProcessMemory(IntPtr processHandle, IntPtr baseAddressToReadFrom,
            [Out] byte[] bufferToReadInto, int numBytesToRead, out int numBytesRead)
        {
            return WindowsAPIDllStuff.ReadProcessMemory(processHandle, baseAddressToReadFrom, bufferToReadInto, numBytesToRead, out numBytesRead);
        }
    }
}
