using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects.GenericWhereClasses
{
    public class ConcreteClass : BaseClass, IToaster
    {
        public override void Fry(string item)
        {
            Debug.Print("We are frying with the concrete class");
            base.Fry(item);
        }

        public void Toast(string item)
        {
            Debug.Print("We are toasting with the concrete class");
        }
    }
}
