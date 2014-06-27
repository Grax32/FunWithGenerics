using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithGenerics.DemoObjects
{
    public class FakeThingFor<T>
    {
        public static int StaticId
        {
            get;
            set;
        }

        public void SetStaticId(int newId)
        {
            StaticId = newId;
        }

        public int GetStaticId()
        {
            return StaticId;
        }
    }
}
