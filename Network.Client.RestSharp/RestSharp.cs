using RestSharp;
using RestSharp.Authenticators;
using Network.Utilidades;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Network.Client
{
    public class RestSharp : ClientGeneric
    {
        private readonly RestClient _client;
        public RestSharp(string basePath, ClientOptions options = null) : base(basePath, options)
        {
            _client = new RestClient(BasePath);
             if (AuthorizationKey != null)
            {
                _client.AddDefaultHeader("authorization", $"Bearer {AuthorizationKey}");
            }
        }

        public override IClient Authorization(string token)
        {
            AuthorizationKey = token;
            _client.AddDefaultHeader("authorization", $"Bearer {AuthorizationKey}");
            return this;
        }

        public override async Task<T> DeleteAsync<T>(string path)
        {
           T data = default(T);
            var request = new RestRequest(path, DataFormat.Json);
            data = await _client.DeleteAsync<T>(request);
            return data;
        }

        public override async Task<T> GetAsync<T>(string path, string query)
        {
            T data = default(T);
            var request = new RestRequest(JoinPathAndQuery(path, query), DataFormat.Json);
            data = await _client.GetAsync<T>(request);
            return data;
        }

        public override async Task<U> PatchAsync<T, U>(string path, T data)
        {
            U dataResult = default(U);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var request = new RestRequest(path, Method.PATCH, DataFormat.Json)
            .AddParameter("application/json", json, ParameterType.RequestBody);

            dataResult = await _client.PatchAsync<U>(request);
            return dataResult;
        }

        public override async Task<U> PostAsync<T, U>(string path, T data)
        {
            U dataResult = default(U);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var request = new RestRequest(path, DataFormat.Json)
            .AddParameter("application/json", json, ParameterType.RequestBody);

            dataResult = await _client.PostAsync<U>(request);
            return dataResult;
        }
    }
}
