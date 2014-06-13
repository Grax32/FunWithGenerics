using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FunWithGenerics.DemoObjects
{
    public class SimpleClass
    {
        public SimpleClass()
        {
            Debug.Print("Constructing Simple Class with no constructor parameters");
        }

        public SimpleClass(string parameter)
        {
            Debug.Print("Constructing Simple Class with parameter=" + parameter);
        }

        public int Id { get; set; }
        public string Description
        {
            get { return _description; }
            set
            {
                Debug.Print(value ?? string.Empty);
                _description = value;
            }
        }
        private string _description;

        public override string ToString()
        {
            return string.Format("SimpleClass: Id={0}, Description={1}", Id, Description);
        }
    }
}
