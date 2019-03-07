using System.IO;
using System.Threading;
using System.Threading.Tasks;

using jcDB.lib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcDB.UnitTests
{
    [TestClass]
    public class SanityTests
    {
        private const string DB_FILENAME = "testing.db";

        [TestCleanup]
        public void CleanUp()
        {
            if (File.Exists(DB_FILENAME))
            {
                File.Delete(DB_FILENAME);
            }
        }

        [TestMethod]
        public void SimpleInsertAndRetrieveNonFireAndForget()
        {
            var key = "testkey";
            var val = "testval";

            var db = new Database();

            db.InsertFireAndForget(key, val);

            var result = db.Get(key);

            Assert.AreEqual(val, result);
        }

        [TestMethod]
        public async Task SimpleInsertAndRetrieveAsync()
        {
            var key = "testkey";
            var val = "testval";

            var db = new Database();

            var insertResult = await db.InsertAsync(key, val);

            Assert.IsTrue(insertResult);

            var result = db.Get(key);

            Assert.AreEqual(val, result);
        }

        [TestMethod]
        public async Task VerifyWriteWorker()
        {
            var key = "testkey";
            var val = "testval";

            var db = new Database(DB_FILENAME, 1);

            var insertResult = await db.InsertAsync(key, val);

            Assert.IsTrue(insertResult);

            Thread.Sleep(1000);

            Assert.IsTrue(File.Exists(DB_FILENAME));
        }
    }
}