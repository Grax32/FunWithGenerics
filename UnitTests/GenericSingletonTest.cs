using FunWithGenerics.DemoObjects;
using FunWithGenerics.Generics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class GenericSingletonTest
    {
        //[TestMethod]
        public void GenericSingletonDemonstration()
        {
            Debug.Print("Not constructed yet");

            var simple = GenericSingleton<SimpleClass>.Instance;
            simple.Id = 7;

            var simple2 = GenericSingleton<SimpleClass>.Instance;

            Assert.IsTrue(simple2.Id == 7);

            Assert.IsTrue(simple == simple2);

        }
    }
}
