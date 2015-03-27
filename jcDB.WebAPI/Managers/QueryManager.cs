using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jcDB.PCL.Transports;

namespace jcDB.WebAPI.Managers {
    public class QueryManager : BaseManager {
        private readonly ServerManager _serverManager;
        private readonly RouterManager _routerManager;

        public QueryManager(ServerManager serverManager, RouterManager routerManager) {
            _serverManager = serverManager;
            _routerManager = routerManager;
        }

        private string findServer(string key) {
            return _routerManager.GetServer(key);
        }

        public object GetObject(string key) {
            var serverKey = findServer(key);

            if (String.IsNullOrEmpty(serverKey)) {
                throw new Exception("Could not find object");
            }

            return _serverManager.GetObject(serverKey, key);
        }

        public bool AddObject(ValueInsertionRequestItem requestItem) {
            return _routerManager.AddEntry(requestItem.ipAddress, requestItem.key);
        }
    }
}