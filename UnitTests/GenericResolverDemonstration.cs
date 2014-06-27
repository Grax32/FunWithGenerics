using FunWithGenerics.DemoObjects.GenericResolverClasses;
using FunWithGenerics.Generics;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

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
            double computeRelativeBaseTicks = 0;

            watch.Start();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = new CustomDbContext();
            }
            watch.Stop();
            //Log("new from Constructor {0} ", FormatTicks(watch.ElapsedTicks));
            computeRelativeBaseTicks = watch.ElapsedTicks;
            Log("new from Constructor {0} ", FormatRelativeUnits(watch.ElapsedTicks / computeRelativeBaseTicks));


            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = new Repository(new CustomDbContext());
            }
            watch.Stop();
            //Log("complex new from Constructor {0} ", FormatTicks(watch.ElapsedTicks));
            Log("complex new from Constructor {0} ", FormatRelativeUnits(watch.ElapsedTicks / computeRelativeBaseTicks));

            //watch.Restart();
            //for (int i = 0; i < oneMillion; i++)
            //{
            //    var myVar = GenericResolver<CustomDbContext>.Resolve();
            //}
            //Log("new from GenericResolver {0} ", FormatTicks(watch.ElapsedTicks));

            //watch.Restart();
            //for (int i = 0; i < oneMillion; i++)
            //{
            //    var myVar = GenericResolver<IRepository>.Resolve();
            //}
            //Log("complex new from GenericResolver {0} ", FormatTicks(watch.ElapsedTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = EmbedIOC.Resolve<ICustomDbContext>();
            }
            //Log("new from EmbedIOC {0} ", FormatTicks(watch.ElapsedTicks));
            Log("new from EmbedIOC {0} ", FormatRelativeUnits(watch.ElapsedTicks / computeRelativeBaseTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = EmbedIOC.Resolve<IRepository>();
            }
            //Log("complex new from EmbedIOC (generic) {0} ", FormatTicks(watch.ElapsedTicks));
            Log("complex new from EmbedIOC (generic) {0} ", FormatRelativeUnits(watch.ElapsedTicks / computeRelativeBaseTicks));

            var typeofICustomDbContext = typeof(ICustomDbContext);

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = EmbedIOC.Resolve(typeofICustomDbContext);
            }
            //Log("new from EmbedIOC (type object) {0} ", FormatTicks(watch.ElapsedTicks));
            Log("new from EmbedIOC (type object) {0} ", FormatRelativeUnits(watch.ElapsedTicks / computeRelativeBaseTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = unity.Resolve<ICustomDbContext>();
            }
            //Log("new from Unity {0} ", FormatTicks(watch.ElapsedTicks));
            Log("new from Unity {0} ", FormatRelativeUnits(watch.ElapsedTicks / computeRelativeBaseTicks));

            watch.Restart();
            for (int i = 0; i < oneMillion; i++)
            {
                var myVar = unity.Resolve<IRepository>();
            }
            //Log("complex new from Unity {0} ", FormatTicks(watch.ElapsedTicks));
            Log("complex new from Unity {0} ", FormatRelativeUnits(watch.ElapsedTicks / computeRelativeBaseTicks));

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

        public static string FormatRelativeUnits(double units)
        {
            return "took " + units.ToString("N4") + " times the basic 'new'";
        }
    }
}
