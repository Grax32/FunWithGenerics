using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunWithGenerics.DemoObjects;
using FunWithGenerics.Generics;
using System.Diagnostics;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class SimpleLazyUnitTest
    {
        //[TestMethod]
        public void SimpleLazyDemonstration()
        {
            Debug.WriteLine("-- Lazy Demonstration --");
            var simpleLazy = new SimpleLazy<SimpleClass>();
            Debug.WriteLine("Not created yet");
            simpleLazy.Value.Description = "New description";
        }
    }
}
