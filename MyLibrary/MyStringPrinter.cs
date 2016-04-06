using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class MyStringPrinter
    {
        public void PrintString()
        {
            string input = Console.ReadLine();
            Console.WriteLine($"you typed {input}");
            Console.ReadLine();
        }
    }
}
