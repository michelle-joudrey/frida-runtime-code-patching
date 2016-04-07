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
            // MyLibrary.dll won't get loaded into the runtime until we use MyLibraryClass,
            // so for the hooker work, we use MyLibraryClass once, and then wait for the 
            // hooker to do it's work.
            MyLibrary.MyLibraryClass myLibraryClassInstance = new MyLibrary.MyLibraryClass();
            bool before = myLibraryClassInstance.MyLibraryMethod();
            Console.WriteLine(before);
            Console.ReadLine();
            if (myLibraryClassInstance.MyLibraryMethod())
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Fail");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
