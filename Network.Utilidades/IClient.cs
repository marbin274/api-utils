using System.Threading.Tasks;

namespace Network.Utilidades
{
    public interface IClient
    {
        IClient Authorization(string token);
        Task<T> DeleteAsync<T>(string path);
        Task<T> GetAsync<T>(string path, string query = null) where T : class;
        Task<U> PostAsync<T, U>(string path, T data) where T: class;
        Task<U> PatchAsync<T, U>(string path, T data) where T: class;

    }
}
