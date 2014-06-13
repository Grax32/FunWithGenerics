using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestProject1
{
    public static class GenericSingleton<T>
    {
        static GenericSingleton()
        {
            InstanceFactory = () => Activator.CreateInstance<T>();
        }

        public static Func<T> InstanceFactory { get; set; }
        public static T Instance { get { return GenericSingletonInstance.Instance; } }
        /// <summary>
        /// Return instance.  Use factory if instance has not yet been created.
        /// factory parameter will be ignored if instance is already constructed.
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static T GetInstance(Func<T> factory)
        {
            InstanceFactory = factory;
            return GenericSingletonInstance.Instance;
        }

        private static class GenericSingletonInstance
        {
            private static readonly T _instance = InstanceFactory();
            public static T Instance { get { return _instance; } }
        }
    }
}
