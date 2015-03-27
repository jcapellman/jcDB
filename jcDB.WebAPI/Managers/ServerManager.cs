using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcDB.WebAPI.Managers {
    public class ServerManager : BaseManager {
        public object GetObject(string serverKey, string key) {
            return new object();
        }

        public bool AddServer(string ipAddress)
        {
            return true;
        }
    }
}