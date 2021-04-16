using System.Threading.Tasks;

namespace Network.Utilidades
{
    public interface IHandler
    {
        IHandler Authorization(string token);
        T Get<T>(string path, string query) where T : class;
        Task<T> GetAsync<T>(string path, string query = null) where T : class;

    }
}
