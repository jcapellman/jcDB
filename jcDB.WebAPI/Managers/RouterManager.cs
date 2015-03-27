using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jcDB.PCL;
using jcDBfs.WIndows.Lib;

namespace jcDB.WebAPI.Managers {
    public class RouterManager : BaseManager {
        private readonly jcDBEntities _dbEntities;

        public RouterManager() {
            _dbEntities = new jcDBEntities(new jcDBfsWindows());
        }
 
        public void AddEntry(string serverKey, string key) {
            _dbEntities.Add(key, serverKey);
        }

        public string GetServer(string key) {
            var serverKey = _dbEntities.Get<string>(key);

            if (String.IsNullOrEmpty(serverKey)) {
                return null;
            }

            return serverKey;
        }
    }
}
