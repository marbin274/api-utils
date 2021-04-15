namespace Network.Utilidades
{
    public interface  IHandler<T> where T : class
    {
        IHandler<T> Authorization(string token);
        T Get(string path, string query);
        T GetAsync(string path, string query);

    }
}
