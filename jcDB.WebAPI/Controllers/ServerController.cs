using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcDB.WebAPI.Controllers {
    public class ServerController : BaseAPIController {
        public bool PUT(string ipAddress) {
            return _serverManager.Add(ipAddress);
        }
    }
}
