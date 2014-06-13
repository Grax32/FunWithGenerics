using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects
{
    public class SimpleWithStaticConstructor
    {
        /// <summary>
        /// This is the static constructor.
        /// It runs once and only once when the type is first accessed
        /// </summary>
        static SimpleWithStaticConstructor()
        {
            Debug.Print("Static constructor running");

            TimeStaticConstructorRan = DateTime.Now;
        }


        public static DateTime TimeStaticConstructorRan { get; private set; }


        public SimpleWithStaticConstructor()
        {
            Debug.Print("Constructing SimpleWithStaticConstructor with no constructor parameters");
        }

        public SimpleWithStaticConstructor(string parameter)
        {
            Debug.Print("Constructing SimpleWithStaticConstructor with parameter=" + parameter);
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
