
namespace Models
{
    public class Response<T, U> where T : class where U : class
    {
        public string code { get; set; }
        public U meta { get; set; }
        public T data { get; set; }
    }
}
