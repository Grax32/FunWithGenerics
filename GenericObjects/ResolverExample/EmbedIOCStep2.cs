using System;
using System.Reflection;

namespace FunWithGenerics.Generics
{
    public static class EmbedIOCStep2<T>
    {
        private static Func<T> _resolveFunction = () => { throw new Exception("No resolver has been registered for the type " + typeof(T).Name); };

        public static void RegisterResolver(Func<T> resolverFunction)
        {
            _resolveFunction = resolverFunction;
        }

        public static T Resolve()
        {
            return _resolveFunction();
        }
    }
}