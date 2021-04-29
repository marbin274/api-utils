using System.Threading.Tasks;

namespace Network.Utilidades
{
    public abstract class ClientGeneric : IClient
    {
        public string BasePath { get; private set; }
        protected readonly string _mediaType = "application/json";
        protected string AuthorizationKey { get; set; }
        public ClientGeneric(string basePath, ClientOptions options = null)
        {
            BasePath = basePath;
            if (options != null && options.Authorization != null)
            {
                AuthorizationKey = options.Authorization;
            }
            if (options != null && options.MediaType != null)
            {
                _mediaType = options.MediaType;
            }
        }

        protected string JoinPathAndQuery(string path, string query){
            return $"{path}{((query != null && query.Length > 0) ? $"?{query}" : "")}";
        }


        public abstract IClient Authorization(string token);
        public abstract Task<T> GetAsync<T>(string path, string query) where T : class;
        public abstract Task<U> PostAsync<T, U>(string path, T data) where T : class;
        public abstract Task<U> PatchAsync<T, U>(string path, T data) where T : class;
        public abstract Task<T> DeleteAsync<T>(string path);
    }

}