using FunWithGenerics.DemoObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.UnitTests
{
    //[TestClass]
    public class StaticConstructorTest
    {
        [TestMethod]
        public void StaticConstructorDemonstration()
        {
            var timeStaticConstructorRan = SimpleWithStaticConstructor.TimeStaticConstructorRan;

            Assert.AreEqual(timeStaticConstructorRan, SimpleWithStaticConstructor.TimeStaticConstructorRan);
        }        

        [TestMethod]
        public void StaticInitializationWithConstructorDemonstration()
        {
            var x = new StaticInitializationWithConstructor();

            Debug.Print(StaticInitializationWithConstructor.ReadFirstValue.ToString());
            Debug.Print(StaticInitializationWithConstructor.ReadSecondValue.ToString());
        }

        [TestMethod]
        public void StaticInitializationWithoutConstructorDemonstration()
        {
            var x = new StaticInitializationWithoutConstructor();

            Debug.Print(StaticInitializationWithoutConstructor.ReadFirstValue.ToString());
            Debug.Print(StaticInitializationWithoutConstructor.ReadSecondValue.ToString());            
        }
    }
}
