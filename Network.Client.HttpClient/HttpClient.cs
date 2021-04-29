using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Network.Utilidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SystemHttp = System.Net.Http;
namespace Network.Client
{
    public class HttpClient : ClientGeneric
    {
        private SystemHttp.HttpClient _client;
        public HttpClient(string basePath, ClientOptions options = null) : base(basePath, options)
        {
            _client = new SystemHttp.HttpClient();
            _client.BaseAddress = new Uri(BasePath);
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(_mediaType));
            if (AuthorizationKey != null)
            {
                _client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", AuthorizationKey);
            }
        }

        public override IClient Authorization(string token)
        {
            AuthorizationKey = token;
            _client.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue("Bearer", AuthorizationKey);
            return this;
        }

        public override async Task<T> DeleteAsync<T>(string path)
        {
            T dataResult = default(T);
            HttpResponseMessage response = await _client.DeleteAsync(path);
            if (response.IsSuccessStatusCode)
            {
                dataResult = await response.Content.ReadAsAsync<T>();
            }
            return dataResult;
        }

        public override async Task<T> GetAsync<T>(string path, string query) where T : class
        {
            T data = default(T);
            HttpResponseMessage response = await _client.GetAsync(JoinPathAndQuery(path, query));
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<T>();
            }
            return data;
        }

        public override async Task<U> PatchAsync<T, U>(string path, T data)
        {
            U dataResult = default(U);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PatchAsync(
               path, content);
            if (response.IsSuccessStatusCode)
            {
                dataResult = await response.Content.ReadAsAsync<U>();
            }
            return dataResult;
        }

        public override async Task<U> PostAsync<T, U>(string path, T data)
        {
            U dataResult = default(U);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(
               path, content);
            if (response.IsSuccessStatusCode)
            {
                dataResult = await response.Content.ReadAsAsync<U>();
            }
            return dataResult;
        }
    }
}
