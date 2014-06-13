using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FunWithGenerics.DemoObjects
{
    public class SimpleClassComplexConstructor
    {
        public SimpleClassComplexConstructor()
        {
            Debug.Print("Constructing Simple Class");
        }

        public SimpleClassComplexConstructor(string parameter)
        {
            Debug.Print("Constructing Simple Class with parameter=" + parameter);
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("SimpleClass: Id={0}, Description={1}", Id, Description);
        }
    }
}
