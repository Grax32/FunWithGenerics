using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunWithGenerics.Generics;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class EmbedIOCStepsTest
    {
        #region ClassesForTest
        public interface IMyInterface { }
        public class MyClass : IMyInterface { }

        public class MyOtherClass : IMyInterface { }
        #endregion

        IMyInterface CreateMyClass()
        {
            return new MyClass();
        }

        [TestMethod]
        public void Z__EmbedIOCStep1Test()
        {
            //EmbedIOCStep1<IMyInterface>.Resolver = () => new MyClass();
            EmbedIOCStep1<IMyInterface>.Resolver = CreateMyClass;

            var result1 = EmbedIOCStep1<IMyInterface>.Resolver();

            EmbedIOCStep1<IMyInterface>.Resolver = () => new MyOtherClass();

            var result2 = EmbedIOCStep1<IMyInterface>.Resolver();
        }

        [TestMethod]
        public void Z__EmbedIOCStep2Test()
        {
            EmbedIOCStep2<IMyInterface>.RegisterResolver(() => new MyClass());

            var result = EmbedIOCStep2<IMyInterface>.Resolve();
        }

        [TestMethod]
        public void Z__EmbedIOCStep3Test()
        {
            //EmbedIOCStep3.Register<IMyInterface>(() => new MyClass());
            EmbedIOCStep3.RegisterType<IMyInterface, MyClass>();

            var result = EmbedIOCStep3.Resolve<IMyInterface>();
        }

        [TestMethod]
        public void Z__EmbedIOCStep4Test()
        {
            var resultType = typeof(IMyInterface);
            EmbedIOCStep4.Register<IMyInterface>(() => new MyClass());
            //EmbedIOCStep4.RegisterType<IMyInterface, MyClass>();

            var result = EmbedIOCStep4.Resolve(resultType);
        }
    }
}
