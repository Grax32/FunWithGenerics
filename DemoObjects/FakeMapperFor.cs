using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithGenerics.DemoObjects
{
    public class FakeMapperFor<T1, T2>
    {
        public static int StaticId { get; set; }

        public void SetStaticId(int newId)
        {
            StaticId = newId;
        }

        public int GetStaticId()
        {
            return StaticId;
        }

        public static Func<T1, T2> Converter { get; set; }
    }
}
