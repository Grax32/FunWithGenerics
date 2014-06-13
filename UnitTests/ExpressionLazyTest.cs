using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunWithGenerics.Generics;
using FunWithGenerics.DemoObjects;
using System.Diagnostics;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class ExpressionLazyTest
    {
        //[TestMethod]
        public void ExpressionLazyDemonstration()
        {
            Debug.WriteLine("-- ExpressionLazy Demonstration --");
            var expressionLazy = new ExpressionLazy<SimpleClass>(() => new SimpleClass("Some Value"));
            Debug.WriteLine("1st Not created yet");
            expressionLazy.Value.Description = "New description";
            Debug.WriteLine("");
        }

        //[TestMethod]
        public void ExpressionLazy2Demonstration()
        {
            Debug.WriteLine("-- ExpressionLazy2 Demonstration --");
            var expressionLazy2 = new ExpressionLazy<SimpleClass>(() => new SimpleClass("Some Other Value"));
            Debug.WriteLine("2nd Not created yet");
            Debug.WriteLine("Still not created");
            expressionLazy2.Value.Description = "Newer description";
            Debug.WriteLine("");
        }
    }
}
