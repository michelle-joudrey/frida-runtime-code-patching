using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringPrinter = new MyLibrary.MyStringPrinter();
            stringPrinter.PrintString("Hello, world");
        }
    }
}
