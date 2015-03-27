using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using jcDB.WebAPI.Managers;

namespace jcDB.WebAPI.Controllers {
    public class BaseAPIController : ApiController {
        protected static RouterManager _routerManager;
        protected static QueryManager _queryManager;
        protected static ServerManager _serverManager;

        public BaseAPIController() {
            if (_routerManager == null) {
                _routerManager = new RouterManager();
            }

            if (_serverManager == null) {
                _serverManager = new ServerManager();
            }

            if (_queryManager == null) {
                _queryManager = new QueryManager(_serverManager, _routerManager);
            }
        }
    }
}
