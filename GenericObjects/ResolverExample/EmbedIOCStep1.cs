using System;
using System.Reflection;

namespace FunWithGenerics.Generics
{
    public static class EmbedIOCStep1<T>
    {
        public static Func<T> Resolver { get; set; }
    }
}