using FunWithGenerics.Generics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace FunWithGenerics.UnitTests
{
    [TestClass]
    public class EmbedIOCTest
    {
        [TestMethod]
        public void Z__EmbedIOCExample()
        {
            EmbedIOC.Register<IDbAccess>(() => new SqlDb(""));

            var result = EmbedIOC.Resolve<IDbAccess>();
            

            Assert.IsInstanceOfType(result, typeof(SqlDb));
        }

        [TestMethod]
        public void Z__EmbedIOCSingleton()
        {
            var singleton = new SqlDb("Singleton");
            EmbedIOC.Register<IDbAccess>(() => singleton);

            var result1 = EmbedIOC.Resolve<IDbAccess>();
            var result2 = EmbedIOC.Resolve(typeof(IDbAccess));

            Assert.ReferenceEquals(result1, result2);
            Assert.ReferenceEquals(singleton, result1);
        }

        [ThreadStatic]
        static IDbAccess _threadStatic;

        static List<IDbAccess> _threadReferenceList = new List<IDbAccess>();

        [TestMethod]
        public void Z__EmbedIOCSharedPerThread()
        {
            EmbedIOC.Register<IDbAccess>(() => _threadStatic ?? (_threadStatic = new SqlDb("SqlDb:" + Thread.CurrentThread.Name)));

            var threadList = new List<Thread>();
            for (int threadCounter = 1; threadCounter <= 10; threadCounter++)
            {
                var newThread = new Thread(new ThreadStart(EmbedIOCSharedPerThread_Worker))
                {
                    Name= threadCounter.ToString()
                };

                threadList.Add(newThread);
            }

            // start threads
            threadList.ForEach(t => t.Start());

            // wait for all thread completion
            threadList.ForEach(t => t.Join());

            _threadReferenceList.ForEach(x =>
                {
                    // assert that 10 different instances were created, one for each thread
                    Assert.AreEqual(1, _threadReferenceList.Count(v => v == x));
                });
        }

        static void EmbedIOCSharedPerThread_Worker()
        {
            Thread.Sleep(100);
            var result1 = EmbedIOC.Resolve<IDbAccess>();
            var result2 = EmbedIOC.Resolve(typeof(IDbAccess));

            Assert.ReferenceEquals(result1, result2);

            _threadReferenceList.Add(result1);

            Debug.Print("Thread: " + Thread.CurrentThread.Name + " uses connection " + ((SqlDb)result1).ConnectionName);
        }

        public interface IDbAccess
        {
            string ConnectionName { get; }
        }

        [DebuggerDisplay("ConnectionName: {ConnectionName}")]
        public class SqlDb : IDbAccess
        {
            private string _cName;

            public SqlDb(string cName)
            {
                _cName = cName;
                Debug.Print("Constructing SqlDb with name " + cName);
            }

            public string ConnectionName { get { return _cName ?? "SQL DB"; } }
        }
    }
}
