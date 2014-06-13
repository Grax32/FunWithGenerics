using FunWithGenerics.DemoObjects.GenericWhereClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects
{
    public class GenericWhereHasBaseClass<T>
        where T : BaseClass
    {
        public void DoSomething(T genericItem)
        {
            genericItem.Fry("eggs");
        }
    }

    public class GenericWhereHasInterface<T>
        where T : IToaster
    {
        public void DoSomething(T genericItem)
        {
            genericItem.Toast("bread");
        }
    }

    public class GenericNoWhere<T>
    {
        public void MakeBreakfast(T genericItem)
        {
            // nothing available
        }
    }
}
