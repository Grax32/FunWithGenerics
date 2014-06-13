using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunWithGenerics.Generics;
using FunWithGenerics.DemoObjects.GenericResolverClasses;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using System.IO;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class GenericResolverDemonstration
    {
        //[TestMethod]
        public void GenericResolverDemo()
        {
            InitializeGenericResolver();

            var repository = GenericResolver<IRepository>.Resolve();

            repository.Save("my item");
        }

        [TestMethod]
        public void Presentation_099_ResolverRelativeSpeedDemo()
        {
            var unity = InitializeUnityResolver();
            InitializeGenericResolver();
            InitializeEmbedIOCResolver();
            var watch = new Stopwatch();
            var oneMillion = 1000000;

            watch.Start();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = new CustomDbContext();
            }
            Log("new from Constructor {0} ", FormatTicks(watch.ElapsedTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = GenericResolver<CustomDbContext>.Resolve();
            }
            Log("new from GenericResolver {0} ", FormatTicks(watch.ElapsedTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = GenericResolver<IRepository>.Resolve();
            }
            Log("complex new from GenericResolver {0} ", FormatTicks(watch.ElapsedTicks));
            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = unity.Resolve<IRepository>();
            }
            Log("complex new from Unity {0} ", FormatTicks(watch.ElapsedTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = EmbedIOC.Resolve<ICustomDbContext>();
            }
            Log("new from EmbedIOC {0} ", FormatTicks(watch.ElapsedTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = EmbedIOC.Resolve<IRepository>();
            }
            Log("complex new from EmbedIOC {0} ", FormatTicks(watch.ElapsedTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = EmbedIOC.Resolve(typeof(ICustomDbContext));
            }
            Log("new from EmbedIOC type object {0} ", FormatTicks(watch.ElapsedTicks));

            Log("");
        }

        void Log(string message, string detail)
        {
            Log(string.Format(message, detail));
        }

        void Log(string message)
        {
            message = message.PadLeft(80);
#if DEBUG
            Debug.Print(message);
#endif
#if !DEBUG
            File.AppendAllText("SpeedDemo.log", message + "\r\n");
#endif
        }

        public void InitializeGenericResolver()
        {
            GenericResolver<ICustomDbContext>.SetResolver(() => new CustomDbContext());
            GenericResolver<IRepository>.SetResolver(() => GenericResolver<Repository>.Resolve());
        }

        public void InitializeEmbedIOCResolver()
        {
            EmbedIOC.Register<ICustomDbContext>(() => new CustomDbContext());
            EmbedIOC.Register<IRepository>(() => new Repository(EmbedIOC.Resolve<ICustomDbContext>()));

            // run once so it will be cached
            var tmp = EmbedIOC.Resolve(typeof(ICustomDbContext));
            var tmp2 = EmbedIOC.Resolve(typeof(IRepository));
        }

        public UnityContainer InitializeUnityResolver()
        {
            var unityContainer = new UnityContainer();
            unityContainer
                .RegisterType<IRepository, Repository>(new InjectionFactory(v => new Repository(v.Resolve<ICustomDbContext>())))
                .RegisterType<ICustomDbContext, CustomDbContext>();
            return unityContainer;
        }

        public static string FormatTicks(long ticks)
        {
            return "took " + ticks.ToString("#,##0") + " ticks";
        }
    }
}
