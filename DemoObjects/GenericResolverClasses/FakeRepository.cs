using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects.GenericResolverClasses
{
    public class FakeRepository : IRepository
    {
        public void Save(string item)
        {
            Debug.Print("Fake saving " + item);
        }
    }
}
