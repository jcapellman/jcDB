using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace jcDB.lib {
    public class jcDBClient {
        private readonly string _baseWebAPIURL;

        public jcDBClient(string baseWebAPIURL = "http://localhost") {
            _baseWebAPIURL = baseWebAPIURL;
        }

        #region Http Client Wrappers
        private async Task<object> Get(string requestURL) {
            var httpClient = new HttpClient();

            var data = await httpClient.GetStringAsync(buildRequestURL(requestURL));

            return JsonConvert.DeserializeObject(data);
        }
        #endregion

        public async Task<T> Put<T>(string requestURL, string content) {
            var httpClient = new HttpClient();

            var data = await httpClient.PutAsync(buildRequestURL(requestURL), new StringContent(content));

            return JsonConvert.DeserializeObject<T>(await data.Content.ReadAsStringAsync());
        }

        private string buildRequestURL(string requestURL) {
            return String.Format("{0}/{1}", _baseWebAPIURL, WebUtility.UrlEncode(requestURL));
        }

        public async Task<object> GetObject(string key) {
            return await Get(String.Format("Query?key={0}", key));
        }

        public async Task<bool> AddServer(string ipAddress) {
            return await Put<bool>("Server", ipAddress);
        }
    }
}