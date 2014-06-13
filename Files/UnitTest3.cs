using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using Microsoft.Practices.Unity;

namespace TestProject1
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethod1()
        {
            Debug.Print("Before InstanceFactory");
            //GenericSingleton<TestClass>.InstanceFactory = () => new TestClass { Id = 7, Description = "Init" };
            Debug.Print("Before InstanceAccess");
            Debug.Print(GenericSingleton<TestClass>.Instance.ToString());
            Debug.Print("Before Id set");
            GenericSingleton<TestClass>.Instance.Id = 45;

            Debug.Print(GenericSingleton<TestClass>.Instance.ToString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            //var s1 = GenericResolver<TestClass>.Resolve();


            GenericResolver<IYY>.SetResolver(() => new YY { Details = "From Factory" });
            GenericResolver<IYY>.AddPropertySetter(v => v.Details, () => DateTime.Now.ToString());
            GenericResolver<IYY>.AddPropertySetter(v => v.XXObject);

            GenericResolver<IXX>.SetResolver(() => new XX { Description = "From Factory" });

            var z1 = GenericResolver<TestClassComplexConstructor>.Resolve();
            Thread.Sleep(1000);
            var z2 = GenericResolver<TestClassComplexConstructor>.Resolve();
            Thread.Sleep(1000);
            var z3 = GenericResolver<TestClassComplexConstructor>.Resolve();
            Thread.Sleep(1000);
            var z4 = GenericResolver<TestClassComplexConstructor>.Resolve();
            Thread.Sleep(1000);
            var z5 = GenericResolver<TestClassComplexConstructor>.Resolve();



            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var zz = GenericResolver<TestClassComplexConstructor>.Resolve();
            }
            Debug.Print("Generic Milliseconds: {0}", stopwatch.ElapsedMilliseconds);
            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var zz = new TestClass();
            }
            Debug.Print("New Milliseconds: {0}", stopwatch.ElapsedMilliseconds);

            var unityObject = new UnityContainer();

            unityObject.RegisterType<IXX, XX>()
                .RegisterType<IYY, YY>()
                .RegisterType<TestClassComplexConstructor, TestClassComplexConstructor>(new InjectionFactory(v => new TestClassComplexConstructor(unityObject.Resolve<IXX>(), unityObject.Resolve<IYY>())));

            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var zz = unityObject.Resolve<TestClassComplexConstructor>();
            }
            Debug.Print("Unity Milliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }

        //[TestMethod]
        //public void TestMethod2()
        //{
        //    Debug.Print("Before InstanceFactory");
        //    GenericSingleton2<TestClass, MarkerType1>.InstanceFactory = () => new TestClass { Id = 7, Description = "Init" };
        //    Debug.Print("Before InstanceAccess");
        //    Debug.Print(GenericSingleton2<TestClass, MarkerType1>.Instance.ToString());
        //    Debug.Print("Before Id set");
        //    GenericSingleton2<TestClass, MarkerType1>.Instance.Id = 45;

        //    Debug.Print(GenericSingleton2<TestClass, MarkerType1>.Instance.ToString());

        //    Debug.Print("Before InstanceFactory");
        //    GenericSingleton2<TestClass, MarkerType2>.InstanceFactory = () => new TestClass { Id = 72, Description = "Init2" };
        //    Debug.Print("Before InstanceAccess");
        //    Debug.Print(GenericSingleton2<TestClass, MarkerType2>.Instance.ToString());
        //    Debug.Print("Before Id set");
        //    GenericSingleton2<TestClass, MarkerType2>.Instance.Id = 452;

        //    Debug.Print(GenericSingleton2<TestClass, MarkerType2>.Instance.ToString());


        //    Debug.Print("Both");
        //    Debug.Print(GenericSingleton2<TestClass, MarkerType1>.Instance.ToString());
        //    Debug.Print(GenericSingleton2<TestClass, MarkerType2>.Instance.ToString());
        //}

        [TestMethod]
        public void TestMethod3()
        {
            //GenericSingleton<MySingleton>.Instance.Id = 93;

            var z = MySingleton.Instance;

            z.Id = 338;

            Assert.AreEqual(338, MySingleton.Instance.Id);
        }

        [TestMethod]
        public void TestMethod4()
        {
            GenericSingleton<ISingleton>.InstanceFactory = () => MyLazySingleton.Instance;

            var singletonInterface = GenericSingleton<ISingleton>.Instance;

            singletonInterface.Id = 338;

            Assert.AreEqual(338, singletonInterface.Id);

            var singletonInterface2 = GenericSingleton<ISingleton>.Instance;

            Assert.AreEqual(338, singletonInterface2.Id);
        }

        class MarkerType1 { }
        class MarkerType2 { }

        class TestClass
        {
            public TestClass()
            {
                //Debug.Print("Created TestClass");
            }

            public int Id { get; set; }
            public string Description { get; set; }

            public override string ToString()
            {
                return string.Format("Id={0}, Description={1}", Id, Description);
            }
        }

        public interface IXX
        {
            string Description { get; set; }
            DateTime InitiatedAt { get; }
        }

        public interface IYY
        {
            string Details { get; set; }
            DateTime InitiatedAt { get; }
            IXX XXObject { get; set; }
        }

        public class XX : IXX
        {
            public DateTime InitiatedAt { get; private set; }
            public XX()
            {
                InitiatedAt = DateTime.Now;
            }

            public string Description { get; set; }
        }

        public class YY : IYY
        {
            public DateTime InitiatedAt { get; private set; }
            public YY()
            {
                InitiatedAt = DateTime.Now;
            }

            public string Details { get; set; }
            public IXX XXObject { get; set; }
        }

        class TestClassComplexConstructor
        {
            public IXX XX { get; set; }
            public IYY YY { get; set; }

            public TestClassComplexConstructor(IXX xxParm, IYY yyParm)
            {
                //Debug.Print("Created TestClass");
                XX = xxParm;
                YY = yyParm;
            }

            public int Id { get; set; }
            public string Description { get; set; }

            public override string ToString()
            {
                return string.Format("Id={0}, Description={1}", Id, Description);
            }
        }

        public class MySingleton
        {
            static MySingleton()
            {
                // configure InstanceFactory with the private constructor
                GenericSingleton<MySingleton>.InstanceFactory = () => new MySingleton();
            }

            private MySingleton() { }

            public static MySingleton Instance { get { return GenericSingleton<MySingleton>.Instance; } }

            public int Id { get; set; }
            public string Address { get; set; }
        }

        public class MyLazySingleton : ISingleton
        {
            private MyLazySingleton() { }
            private static readonly Lazy<MyLazySingleton> _lazyForInstance = new Lazy<MyLazySingleton>(() => new MyLazySingleton());
            public static MyLazySingleton Instance { get { return _lazyForInstance.Value; } }

            public int Id { get; set; }
            public string Address { get; set; }
        }

        public interface ISingleton
        {
            int Id { get; set; }
            string Address { get; set; }
        }
    }
}
