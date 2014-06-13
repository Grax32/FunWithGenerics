using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.UnitTests
{
    public class Dynomite
    {
        public static Assembly Assembly { get { return _dynamicAssembly; } }

        private static AssemblyBuilder _dynamicAssembly = CreateDynamicAssembly();
        private static ModuleBuilder _defaultModule { get { return _dynamicAssembly.Modules.OfType<ModuleBuilder>().First(); } }

        private static AssemblyBuilder CreateDynamicAssembly()
        {
            var assemblyName = new AssemblyName("DynomiteDynamicAssembly");
            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            assembly.DefineDynamicModule("DynomiteDynamicModule");
            return assembly;
        }

        public static class DynoType<TBase>
        {
            public static void OverrideProperty<TProp>(Expression<Func<TBase, TProp>> property, Func<TBase, TProp> implementation)
            {

            }

            public static void OverrideMethod<TMethodReturn>(Expression<Func<TBase, TMethodReturn>> property, Func<TBase, TBase, TMethodReturn> implementation)
            {

            }

            public static void ImplementInterface<TInterfaceType>()
            {

            }
        }
    }
}
