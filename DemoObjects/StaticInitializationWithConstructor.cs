using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects
{
    public class StaticInitializationWithConstructor
    {
        static StaticInitializationWithConstructor()
        { }

        static DateTime _SomeValueFirst = InitializeSomeFirstValue();
        static int _SomeValueSecond = InitializeSomeSecondValue();
        static DateTime InitializeSomeFirstValue()
        {
            Debug.Print("WC: Initializing _SomeValueFirst ({0})", DateTime.Now);
            return DateTime.Now;
        }

        static int InitializeSomeSecondValue()
        {
            Debug.Print("WC: Initializing _SomeValueSecond ({0})", DateTime.Now);
            return DateTime.Now.Second;
        }

        public static DateTime ReadFirstValue { get { return _SomeValueFirst; } }

        public static int ReadSecondValue { get { return _SomeValueSecond; } }
    }
}
