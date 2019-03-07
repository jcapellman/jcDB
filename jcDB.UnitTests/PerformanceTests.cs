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

        [TestMethod]
        public void InsertTen()
        {
            var data = Initialize(1000);

            var start = DateTime.Now;

            var db = new Database(PERF_DB_FILENAME);

            foreach (var key in data.Keys)
            {
                db.InsertFireAndForget(key, data[key]);    
            }

            Console.WriteLine(DateTime.Now.Subtract(start).TotalSeconds);
        }

        [TestMethod]
        public void InsertTenBulk()
        {
            var data = Initialize(1000);

            var start = DateTime.Now;

            var db = new Database(PERF_DB_FILENAME);

            db.InsertBulk(data);

            Console.WriteLine(DateTime.Now.Subtract(start).TotalSeconds);
        }
    }
}
