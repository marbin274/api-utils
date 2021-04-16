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


        public IClient Authorization(string token)
        {
            AuthorizationKey = token;
            return this;
        }
        public abstract T Get<T>(string path, string query) where T : class;
        public abstract Task<T> GetAsync<T>(string path, string query) where T : class;


    }

}