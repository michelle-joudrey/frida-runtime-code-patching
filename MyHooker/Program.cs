using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MyHooker
{
    class Program
    {
        static void Main(string[] args)
        {
            string myAppPath = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\MyApp\\bin\\Debug\\MyApp.exe";
            WindowsAPI.CreateProcess(myAppPath);
            Process process = WindowsAPI.GetProcess("MyApp");
            IntPtr processHandle = WindowsAPI.GetProcessHandle(process);

            IntPtr moduleBaseAddr = (IntPtr)0; // ?
            int moduleSize = 0;  // ?

            int numBytesRead = 0;
            byte[] buffer = null;
            var ret = WindowsAPI.ReadProcessMemory(processHandle, moduleBaseAddr, buffer, moduleSize, out numBytesRead);

            Console.ReadLine();
        }
    }
}
