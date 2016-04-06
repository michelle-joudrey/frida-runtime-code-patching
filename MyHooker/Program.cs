using System;
using System.IO;
using System.Runtime.InteropServices;

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

namespace MyHooker
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(
            string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
            bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
            string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo,out PROCESS_INFORMATION lpProcessInformation);

        static void Main(string[] args)
        {
            // source: https://blogs.msdn.microsoft.com/thottams/2006/08/11/accessing-createprocess-from-c-and-vb-net/
            STARTUPINFO si = new STARTUPINFO();
            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
            string myAppPath = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\MyApp\\bin\\Debug\\MyApp.exe";
            CreateProcess(myAppPath, null, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref si, out pi);
            Console.ReadLine();
        }
    }
}
