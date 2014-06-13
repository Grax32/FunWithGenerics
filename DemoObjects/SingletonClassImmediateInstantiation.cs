using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects
{
    public class SingletonClassImmediateInstantiation
    {
        // Private constructor prevents instantiation of class
        private SingletonClassImmediateInstantiation() { }

        // Private constructor accessible within class
        // Instance will be constructed when class is initialized
        private static SingletonClassImmediateInstantiation _instance = new SingletonClassImmediateInstantiation();

        public static SingletonClassImmediateInstantiation Instance { get { return _instance; } }

        public void Log(string message)
        {
            // do logging stuff
        }
    }
}
