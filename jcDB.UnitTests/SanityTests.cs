using System.Threading.Tasks;

using jcDB.lib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcDB.UnitTests
{
    [TestClass]
    public class SanityTests
    {
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
    }
}