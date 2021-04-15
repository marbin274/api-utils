namespace Network.Utilidades
{
    public abstract class HandlerGeneric<T> : IHandler<T> where T : class
    {
        public string BasePath { get; private set; }
        private string _authorization { get; set; }
        public HandlerGeneric(string basePath)
        {
            BasePath = basePath;
        }

        public IHandler<T> Authorization(string token)
        {
            _authorization = token;
            return this;
        }
        public abstract T Get(string path, string query);
        public abstract T GetAsync(string path, string query);


    }

}