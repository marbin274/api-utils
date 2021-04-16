using System.Threading.Tasks;

namespace Network.Utilidades
{
    public abstract class HandlerGeneric : IHandler
    {
        public string BasePath { get; private set; }
        private string _authorization { get; set; }
        public HandlerGeneric(string basePath)
        {
            BasePath = basePath;
        }

        public IHandler Authorization(string token)
        {
            _authorization = token;
            return this;
        }
        public abstract T Get<T>(string path, string query) where T : class;
        public abstract Task<T> GetAsync<T>(string path, string query) where T : class;


    }

}