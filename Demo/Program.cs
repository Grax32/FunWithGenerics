using FunWithGenerics.UnitTests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWriterTraceListener myWriter = new TextWriterTraceListener(System.Console.Out);
            Debug.Listeners.Add(myWriter);

            var test = new SimpleLazyUnitTest();
            test.SimpleLazyDemonstration();

            Console.ReadKey();
        }
    }
}
