using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using jcDB.lib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcDB.UnitTests
{
    [TestClass]
    public class PerformanceTests
    {
        private string PERF_DB_FILENAME = "perf.db";

        private Dictionary<string, object> Initialize(int dataSize)
        {
            PERF_DB_FILENAME = $"perf_{dataSize}.db";

            var dict = new Dictionary<string, object>();

            for (var x = 0; x < dataSize; x++)
            {
                dict.Add(x.ToString(), x);
            }

            return dict;
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(PERF_DB_FILENAME))
            {
                File.Delete(PERF_DB_FILENAME);
            }
        }

        [DataTestMethod]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        [DataRow(20)]
        public void Insert(int TimeToRunSeconds)
        {
            var db = new Database();

            var x = 0;

            var sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed < TimeSpan.FromSeconds(TimeToRunSeconds))
            {
                db.InsertFireAndForget(x.ToString(), x);
                x++;
            }

            sw.Stop();

            Console.WriteLine($"Throughput {x/TimeToRunSeconds}/s");

            Assert.IsTrue(x > TimeToRunSeconds * 10);
        }
    }
}