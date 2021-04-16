using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Network.Utilidades;
using SystemHttp = System.Net.Http;
namespace Network.Client
{
    public class HttpClient : HandlerGeneric
    {
        private SystemHttp.HttpClient _client;
        public HttpClient(string basePath) : base(basePath)
        {
            _client = new SystemHttp.HttpClient();
            _client.BaseAddress = new Uri(BasePath);
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override T Get<T>(string path, string query)
        {
            throw new NotImplementedException();
        }

        public override async Task<T> GetAsync<T>(string path, string query) where T : class
        {
            T data = default(T);
            HttpResponseMessage response = await _client.GetAsync($"{path}{((query != null && query.Length > 0) ? $"?{query}" : "")}");
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<T>();
            }
            return data;
        }
    }
}
