using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FunWithGenerics.Generics
{
    public static class EmbedIOC
    {
        private readonly static MethodInfo ResolveMethod = typeof(EmbedIOC).GetMethod("Resolve", new Type[] { });
        private readonly static Dictionary<Type, Func<object>> ResolverCache = new Dictionary<Type, Func<object>>();

        public static T Resolve<T>() { return Resolver<T>._resolveFunction(); }

        public static object Resolve(Type type)
        {
            Func<object> resolver;

            if (!ResolverCache.TryGetValue(type, out resolver))
            {
                lock (ResolverCache)
                {
                    resolver = (ResolverCache[type] = FuncFromMethodInfo(ResolveMethod.MakeGenericMethod(type)));
                }
            }
            return resolver();
        }

        private static Func<object> FuncFromMethodInfo(MethodInfo method)
        {
            return (Func<object>)Expression.Lambda<Func<object>>(Expression.Convert(Expression.Call(method), typeof(object))).Compile();
        }

        public static void Register<T>(Func<T> resolver)
        {
            Resolver<T>._resolveFunction = resolver;
        }

        private static class Resolver<T>
        {
            public static Func<T> _resolveFunction = () => { throw new Exception("No resolver has been registered for the type " + typeof(T).Name); };
        }
    }
}
