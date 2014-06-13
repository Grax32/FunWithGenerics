using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects.GenericWhereClasses
{
    public abstract class BaseClass
    {
        public virtual void Fry(string item)
        {
            Debug.Print("We are frying with the base class");
        }
    }
}
