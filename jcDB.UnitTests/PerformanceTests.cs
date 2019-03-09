using System;
using System.Collections.Generic;
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
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        [DataRow(100000)]
        public void Insert(int runSize)
        {
            var data = Initialize(runSize);

            var start = DateTime.Now;

            var db = new Database(true, PERF_DB_FILENAME);

            foreach (var key in data.Keys)
            {
                db.InsertFireAndForget(key, data[key]);    
            }

            Console.WriteLine(DateTime.Now.Subtract(start).TotalSeconds);

            Assert.IsTrue(runSize == db.GetAll().Count);
        }
    }
}