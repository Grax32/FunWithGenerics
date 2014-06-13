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
            SimpleGenericResolver<ISimpleType>.InstanceCreateFunction = () => new SimpleConcreteType();




            var myInterfaceInstanceReference = SimpleGenericResolver<ISimpleType>.InstanceCreateFunction.Invoke();

            Debug.WriteLine("Road Length: {0}", myInterfaceInstanceReference.GetRoadLength().ToString());
            myInterfaceInstanceReference.UpdateRoadLength(99);
            Debug.WriteLine("Road Length: {0}", myInterfaceInstanceReference.GetRoadLength().ToString());
            Debug.WriteLine("Get SimpleClass: {0}", myInterfaceInstanceReference.GetSimpleClassForId(44).ToString());

            myInterfaceInstanceReference = null;





            SimpleGenericResolver<ISimpleType>.RegisterType<OtherSimpleConcreteType>();

            var myOtherInterface = SimpleGenericResolver<ISimpleType>.InstanceCreateFunction.Invoke();

            Debug.WriteLine("Road Length: {0}", myOtherInterface.GetRoadLength().ToString());
            myOtherInterface.UpdateRoadLength(99);
            Debug.WriteLine("Road Length: {0}", myOtherInterface.GetRoadLength().ToString());
            Debug.WriteLine("Get SimpleClass: {0}", myOtherInterface.GetSimpleClassForId(44).ToString());

            string constructorParm = "ChangeMe";

            SimpleGenericResolver<ISimpleType>.InstanceCreateFunction = () => new OtherSimpleConcreteType(constructorParm);

            constructorParm = "Changed";

            var myOtherInterface2 = SimpleGenericResolver<ISimpleType>.InstanceCreateFunction.Invoke();

            Debug.WriteLine("Road Length: {0}", myOtherInterface2.GetRoadLength().ToString());
            myOtherInterface2.UpdateRoadLength(99);
            Debug.WriteLine("Road Length: {0}", myOtherInterface2.GetRoadLength().ToString());
            Debug.WriteLine("Get SimpleClass: {0}", myOtherInterface2.GetSimpleClassForId(44).ToString());
        }

        [TestMethod]
        public void Presentation_012_SimpleResolverDemonstration2()
        {
            SimpleGenericResolver<IGenericResolverLogger>.InstanceCreateFunction = () => new GenericResolverDebugLogger("Constructor Prefix");

            var myOtherInterface = SimpleGenericResolver<IGenericResolverLogger>.InstanceCreateFunction.Invoke();
            myOtherInterface.Log("Logging Error");



            SimpleGenericResolver<IGenericResolverLogger>.RegisterType<GenericResolverConsoleLogger>();

            var myInterface = SimpleGenericResolver<IGenericResolverLogger>.InstanceCreateFunction.Invoke();

            myInterface.Log("Logging Another Error");


        }

        public interface IGenericResolverLogger
        {
            void Log(string errorMessage);
        }

        public class GenericResolverConsoleLogger : IGenericResolverLogger
        {
            public void Log(string errorMessage)
            {
                Debug.Print("GenericResolverConsoleLogger: " + errorMessage);
            }
        }

        public class GenericResolverDebugLogger : IGenericResolverLogger
        {
            readonly string _prefix;

            public GenericResolverDebugLogger(string prefix)
            {
                _prefix = prefix;
            }

            public void Log(string errorMessage)
            {
                Debug.Print(string.Format("GenericResolverDebugLogger: {0} {1}", _prefix, errorMessage));
            }
        }
    }
}
