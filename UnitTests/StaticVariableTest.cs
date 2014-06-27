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
            FakeThing.StaticId = 1;
            FakeThing.StaticId = 2;
            FakeThing.StaticId = 3;
            FakeThing.StaticId = 4;

            Assert.AreEqual(4, FakeThing.StaticId);

            var instanceOne = new FakeThing();
            var instanceTwo = new FakeThing();

            instanceOne.SetStaticId(499);
            Assert.AreEqual(499, FakeThing.StaticId);
            Assert.AreEqual(499, instanceOne.GetStaticId());
            Assert.AreEqual(499, instanceTwo.GetStaticId());
        }

        [TestMethod]
        public void Presentation_002_StaticGenericVariablesDemonstration()
        {
            FakeThingFor<int>.StaticId = 1;
            FakeThingFor<SimpleClass>.StaticId = 2;

            Assert.AreEqual(1, FakeThingFor<int>.StaticId);
            Assert.AreEqual(2, FakeThingFor<SimpleClass>.StaticId);

            // create new instances of generic type
            var fakeForIntInstance = new FakeThingFor<int>();
            var fakeForSimpleClassInstance = new FakeThingFor<SimpleClass>();

            // each instance uses the appropriate static variable 
            fakeForIntInstance.SetStaticId(5);
            fakeForSimpleClassInstance.SetStaticId(7);

            Assert.AreEqual(5, FakeThingFor<int>.StaticId);
            Assert.AreEqual(5, fakeForIntInstance.GetStaticId());

            var fakeForIntInstance2 = new FakeThingFor<int>();
            Assert.AreEqual(5, fakeForIntInstance2.GetStaticId());

            

            Assert.AreEqual(7, FakeThingFor<SimpleClass>.StaticId);
            Assert.AreEqual(7, fakeForSimpleClassInstance.GetStaticId());
        }

        public void Presentation_002_StaticGenericVariablesDemonstrationOld()
        {
            FakeThingFor<int>.StaticId = 1;
            FakeThingFor<string>.StaticId = 2;
            FakeThingFor<SimpleClass>.StaticId = 3;
            FakeThingFor<DateTime>.StaticId = 4;

            Assert.AreEqual(1, FakeThingFor<int>.StaticId);
            Assert.AreEqual(2, FakeThingFor<string>.StaticId);
            Assert.AreEqual(3, FakeThingFor<SimpleClass>.StaticId);
            Assert.AreEqual(4, FakeThingFor<DateTime>.StaticId);

            // create new instances of generic type
            var fakeForIntInstance = new FakeThingFor<int>();
            var fakeForStringInstance = new FakeThingFor<string>();
            var fakeForSimpleClassInstance = new FakeThingFor<SimpleClass>();
            var fakeForDateTime = new FakeThingFor<DateTime>();

            // each instance uses the appropriate static variable 
            fakeForIntInstance.SetStaticId(5);
            fakeForStringInstance.SetStaticId(6);
            fakeForSimpleClassInstance.SetStaticId(7);
            fakeForDateTime.SetStaticId(8);

            Assert.AreEqual(5, FakeThingFor<int>.StaticId);
            Assert.AreEqual(5, fakeForIntInstance.GetStaticId());

            var fakeForIntInstance2 = new FakeThingFor<int>();
            Assert.AreEqual(5, fakeForIntInstance2.GetStaticId());



            Assert.AreEqual(6, FakeThingFor<string>.StaticId);
            Assert.AreEqual(6, fakeForStringInstance.GetStaticId());

            Assert.AreEqual(7, FakeThingFor<SimpleClass>.StaticId);
            Assert.AreEqual(7, fakeForSimpleClassInstance.GetStaticId());

            Assert.AreEqual(8, FakeThingFor<DateTime>.StaticId);
            Assert.AreEqual(8, fakeForDateTime.GetStaticId());
        }

        [TestMethod]
        public void Presentation_003_StaticGenericVariablesDemonstration()
        {
            FakeMapperFor<int, int>.StaticId = 1;
            FakeMapperFor<int, string>.StaticId = 2;

            Assert.AreEqual(1, FakeMapperFor<int, int>.StaticId);
            Assert.AreEqual(2, FakeMapperFor<int, string>.StaticId);

            var instanceInt = new FakeMapperFor<int, int>();
            var instanceString = new FakeMapperFor<int, string>();

            instanceInt.SetStaticId(5);
            instanceString.SetStaticId(6);

            Assert.AreEqual(5, FakeMapperFor<int, int>.StaticId);
            Assert.AreEqual(5, instanceInt.GetStaticId());

            var otherInstanceInt = new FakeMapperFor<int, int>();
            Assert.AreEqual(5, otherInstanceInt.GetStaticId());



            FakeMapperFor<int, string>.Converter = t => t.ToString();
            var convertResult = FakeMapperFor<int, string>.Converter(44);
            Assert.AreEqual("44", convertResult);

            FakeMapperFor<int, string>.Converter = t => string.Format("The answer is {0}", t);
            var convertResult2 = FakeMapperFor<int, string>.Converter(44);
            Assert.AreEqual("The answer is 44", convertResult2);


        }
    }
}
