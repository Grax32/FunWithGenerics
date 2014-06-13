using System;
using System.Reflection;

namespace FunWithGenerics.Generics
{
    public static class EmbedIOCStep4
    {
        private readonly static object[] emptyObj
            = new object[] { };

        private readonly static MethodInfo ResolveMethod
            = typeof(EmbedIOCStep4)
            .GetMethod("Resolve", new Type[] { });

        public static object Resolve(Type type)
        {
            return ResolveMethod
                .MakeGenericMethod(type)
                .Invoke(null, emptyObj);
        }

        public static T Resolve<T>()
        {
            return Resolver<T>.Resolve();
        }

        public static void Register<T>(Func<T> resolver)
        {
            Resolver<T>.RegisterResolver(resolver);
        }

        public static void RegisterType<T, TConcrete>()
        where TConcrete : T, new()
        {
            //Register(() => new TConcrete());
            Register<T>(() => new TConcrete());
        }

        private static class Resolver<T>
        {
            private static Func<T> _resolveFunction = () =>
            {
                throw new Exception("No resolver has been registered for the type " + typeof(T).Name);
            };

            public static void RegisterResolver(Func<T> resolverFunction)
            {
                _resolveFunction = resolverFunction;
            }

            public static T Resolve() { return _resolveFunction(); }
        }
    }
}