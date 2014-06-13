using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class DynomiteTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var x = new Dynomite.DynoType<MyBaseClass>();

            Dynomite.DynoType<MyBaseClass>.OverrideMethod(
                v => v.MyMethod(),
                (vThis, vBase) =>
                {
                    switch (vThis.MyProperty.ToUpper())
                    {
                        case "UTC":
                            return DateTime.UtcNow;
                        case "LOCAL":
                            return DateTime.Now;
                        case "FUTURE":
                            return DateTime.Now.AddYears(30);
                        default:
                            return vBase.MyMethod();
                    }
                });
            Dynomite.DynoType<MyBaseClass>.OverrideProperty(v => v.MyProperty, v => "Overridden");
        }

        public class MyBaseClass
        {
            public virtual string MyProperty { get; set; }

            public virtual DateTime MyMethod() { return DateTime.Now; }


            private static DateTime MyMethodOverrider(MyBaseClass vThis)
            {
                switch (vThis.MyProperty.ToUpper())
                {
                    case "UTC":
                        return DateTime.UtcNow;
                    case "LOCAL":
                        return DateTime.Now;
                    case "FUTURE":
                        return DateTime.Now.AddYears(30);
                    default:
                        return MyMethodBaseCall(vThis);
                }
            }

            private static DateTime MyMethodBaseCall(MyBaseClass vThis)
            {
                return vThis.MyMethod();
            }
        }
    }
}
