using FunWithGenerics.DemoObjects;
using FunWithGenerics.Generics;
using FunWithGenerics.InterfacesAndDataContainers.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class SimpleGenericResolverTest
    {
        [TestMethod]
        public void Presentation_011_SimpleResolverDemonstration()
        {
            Debug.WriteLine("--Resolver Demonstration--");
            SimpleGenericResolver<IMyInterface>.InstanceCreateFunction = 
                () => new MyConcreteType();




            var myInterfaceInstanceReference = 
                SimpleGenericResolver<IMyInterface>.InstanceCreateFunction.Invoke();

            Debug.WriteLine("Road Length: {0}", myInterfaceInstanceReference.GetRoadLength().ToString());
            myInterfaceInstanceReference.UpdateRoadLength(99);
            Debug.WriteLine("Road Length: {0}", myInterfaceInstanceReference.GetRoadLength().ToString());
            Debug.WriteLine("Get SimpleClass: {0}", myInterfaceInstanceReference.GetSimpleClassForId(44).ToString());

            myInterfaceInstanceReference = null;





            SimpleGenericResolver<IMyInterface>.RegisterType<OtherSimpleConcreteType>();

            var myOtherInterface = SimpleGenericResolver<IMyInterface>.InstanceCreateFunction.Invoke();

            Debug.WriteLine("Road Length: {0}", myOtherInterface.GetRoadLength().ToString());
            myOtherInterface.UpdateRoadLength(99);
            Debug.WriteLine("Road Length: {0}", myOtherInterface.GetRoadLength().ToString());
            Debug.WriteLine("Get SimpleClass: {0}", myOtherInterface.GetSimpleClassForId(44).ToString());

            string constructorParm = "ChangeMe";

            SimpleGenericResolver<IMyInterface>.InstanceCreateFunction = () => new OtherSimpleConcreteType(constructorParm);

            constructorParm = "Changed";

            var myOtherInterface2 = SimpleGenericResolver<IMyInterface>.InstanceCreateFunction.Invoke();

            Debug.WriteLine("Road Length: {0}", myOtherInterface2.GetRoadLength().ToString());
            myOtherInterface2.UpdateRoadLength(99);
            Debug.WriteLine("Road Length: {0}", myOtherInterface2.GetRoadLength().ToString());
            Debug.WriteLine("Get SimpleClass: {0}", myOtherInterface2.GetSimpleClassForId(44).ToString());
        }

        [TestMethod]
        public void Presentation_012_SimpleResolverDemonstration2()
        {            
            SimpleGenericResolver<IGenericResolverLogger>.RegisterType<MyConsoleLogger>();

            var myInterface = SimpleGenericResolver<IGenericResolverLogger>.InstanceCreateFunction.Invoke();

            myInterface.Log("This should be logged using the MyConsoleLogger");


            SimpleGenericResolver<IGenericResolverLogger>.InstanceCreateFunction = () => new MyDebugLogger("Constructor Prefix");
            
            var myOtherInterface = SimpleGenericResolver<IGenericResolverLogger>.InstanceCreateFunction.Invoke();
            myOtherInterface.Log("This should be logged using the MyDebugLogger");

        }

        public interface IGenericResolverLogger
        {
            void Log(string errorMessage);
        }

        public class MyConsoleLogger : IGenericResolverLogger
        {
            public void Log(string errorMessage)
            {
                Debug.Print("MyConsoleLogger: " + errorMessage);
            }
        }

        public class MyDebugLogger : IGenericResolverLogger
        {
            readonly string _prefix;

            public MyDebugLogger(string prefix)
            {
                _prefix = prefix;
            }

            public void Log(string errorMessage)
            {
                Debug.Print(string.Format("MyDebugLogger: {0} {1}", _prefix, errorMessage));
            }
        }
    }
}
