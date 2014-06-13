using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunWithGenerics.DemoObjects;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class StaticVariableTest
    {
        [TestMethod]
        public void Presentation_001_StaticVariablesDemonstration()
        {
            StaticVariableNonGeneric.StaticId = 1;
            StaticVariableNonGeneric.StaticId = 2;
            StaticVariableNonGeneric.StaticId = 3;
            StaticVariableNonGeneric.StaticId = 4;

            Assert.AreEqual(4, StaticVariableNonGeneric.StaticId);

            var instanceOne = new StaticVariableNonGeneric();
            var instanceTwo = new StaticVariableNonGeneric();

            instanceOne.SetStaticId(499);
            Assert.AreEqual(499, StaticVariableNonGeneric.StaticId);
            Assert.AreEqual(499, instanceOne.GetStaticId());
            Assert.AreEqual(499, instanceTwo.GetStaticId());
        }

        [TestMethod]
        public void Presentation_002_StaticGenericVariablesDemonstration()
        {
            StaticVariableGeneric<int>.StaticId = 1;
            StaticVariableGeneric<string>.StaticId = 2;
            StaticVariableGeneric<SimpleClass>.StaticId = 3;
            StaticVariableGeneric<StaticVariableTest>.StaticId = 4;

            Assert.AreEqual(1, StaticVariableGeneric<int>.StaticId);
            Assert.AreEqual(2, StaticVariableGeneric<string>.StaticId);
            Assert.AreEqual(3, StaticVariableGeneric<SimpleClass>.StaticId);
            Assert.AreEqual(4, StaticVariableGeneric<StaticVariableTest>.StaticId);

            Assert.IsTrue(StaticVariableGeneric<int>.StaticId != StaticVariableGeneric<string>.StaticId);
            Assert.IsTrue(StaticVariableGeneric<string>.StaticId != StaticVariableGeneric<SimpleClass>.StaticId);
            Assert.IsTrue(StaticVariableGeneric<SimpleClass>.StaticId != StaticVariableGeneric<StaticVariableTest>.StaticId);

            var instanceInt = new StaticVariableGeneric<int>();
            var instanceString = new StaticVariableGeneric<string>();
            var instanceSimpleClass = new StaticVariableGeneric<SimpleClass>();
            var instanceStaticVariableDemonstration = new StaticVariableGeneric<StaticVariableTest>();

            instanceInt.SetStaticId(5);
            instanceString.SetStaticId(6);
            instanceSimpleClass.SetStaticId(7);
            instanceStaticVariableDemonstration.SetStaticId(8);

            Assert.AreEqual(5, StaticVariableGeneric<int>.StaticId);
            Assert.AreEqual(5, instanceInt.GetStaticId());

            var otherInstanceInt = new StaticVariableGeneric<int>();
            Assert.AreEqual(5, otherInstanceInt.GetStaticId());



            Assert.AreEqual(6, StaticVariableGeneric<string>.StaticId);
            Assert.AreEqual(6, instanceString.GetStaticId());

            Assert.AreEqual(7, StaticVariableGeneric<SimpleClass>.StaticId);
            Assert.AreEqual(7, instanceSimpleClass.GetStaticId());

            Assert.AreEqual(8, StaticVariableGeneric<StaticVariableTest>.StaticId);
            Assert.AreEqual(8, instanceStaticVariableDemonstration.GetStaticId());
        }

        [TestMethod]
        public void Presentation_003_StaticGenericVariablesDemonstration()
        {
            StaticVariableGenericTwo<int, int>.StaticId = 1;
            StaticVariableGenericTwo<int, string>.StaticId = 2;

            Assert.AreEqual(1, StaticVariableGenericTwo<int, int>.StaticId);
            Assert.AreEqual(2, StaticVariableGenericTwo<int, string>.StaticId);

            var instanceInt = new StaticVariableGenericTwo<int, int>();
            var instanceString = new StaticVariableGenericTwo<int, string>();

            instanceInt.SetStaticId(5);
            instanceString.SetStaticId(6);

            Assert.AreEqual(5, StaticVariableGenericTwo<int, int>.StaticId);
            Assert.AreEqual(5, instanceInt.GetStaticId());

            var otherInstanceInt = new StaticVariableGenericTwo<int, int>();
            Assert.AreEqual(5, otherInstanceInt.GetStaticId());



            StaticVariableGenericTwo<int, string>.Converter = t => t.ToString();
            var convertResult = StaticVariableGenericTwo<int, string>.Converter(44);
            Assert.AreEqual("44", convertResult);

            StaticVariableGenericTwo<int, string>.Converter = t => string.Format("The answer is {0}", t);
            var convertResult2 = StaticVariableGenericTwo<int, string>.Converter(44);
            Assert.AreEqual("The answer is 44", convertResult2);


        }
    }
}
