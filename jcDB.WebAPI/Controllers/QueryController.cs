using jcDB.PCL.Transports;

namespace jcDB.WebAPI.Controllers {
    public class QueryController : BaseAPIController {
        public object GET(string key) {
            return _queryManager.GetObject(key);
        }

        public bool POST(ValueInsertionRequestItem requestItem) {
            return _queryManager.AddObject(requestItem);
        }
    }
}