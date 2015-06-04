using System.Collections.Concurrent;

namespace jcDB.PCL {
    public class jcDBEntities {
        private ConcurrentDictionary<string, object> _database;
        private readonly jcDBFS _dbfs;

        public jcDBEntities(jcDBFS dbFS) {
            _database = new ConcurrentDictionary<string, object>();

            _dbfs = dbFS;
        }

        private string hashObject(object obj) {
            return obj.GetHashCode().ToString();
        }

        public string Add(object obj) {
            return addObject(hashObject(obj), obj);
        }

        public string Add(string key, object obj) {
            return addObject(key, obj);
        }

        private string addObject(string key, object obj) {
            _database[key] = obj;

            return key;
        }

        private void Delete(string key) {
            _database = _database.Remove(key);
        }
        
        public T Get<T>(string key) {
            if (_database.ContainsKey(key)) {
                return (T)_database[key];
            }

            return default(T);
        }

        public bool WriteFS() {
            return _dbfs.WriteDB();
        }
    }
}