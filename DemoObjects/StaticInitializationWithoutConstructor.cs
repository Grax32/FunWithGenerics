using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects
{
    public class StaticInitializationWithoutConstructor
    {
        static DateTime _SomeValueFirst = InitializeSomeFirstValue();
        static int _SomeValueSecond= InitializeSomeSecondValue();
        static DateTime InitializeSomeFirstValue()
        {
            Debug.Print("WO: Initializing _SomeValueFirst");
            return DateTime.Now;
        }
        
        static int InitializeSomeSecondValue()
        {
            Debug.Print("WO: Initializing _SomeValueSecond");
            return DateTime.Now.Second;
        }

        public static DateTime ReadFirstValue { get { return _SomeValueFirst; } }

        public static int ReadSecondValue { get { return _SomeValueSecond; } }
    }
}
