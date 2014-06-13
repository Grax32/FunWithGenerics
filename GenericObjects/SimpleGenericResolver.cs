using System;

namespace FunWithGenerics.Generics
{
    public static class SimpleGenericResolver<TInterfaceType>
        where TInterfaceType : class
    {
        public static Func<TInterfaceType> InstanceCreateFunction { get; set; }

        public static void RegisterType<TConcreteType>()
            where TConcreteType : TInterfaceType, new()
        {
            InstanceCreateFunction = () => new TConcreteType();
        }
    }
}