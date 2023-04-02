using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.HttpClientService
{
    public interface IYattHttpClient<T> where T : class
    {
        //Task PostAsync(string url, string content);
        Task<T> PostAsync(string url, T item);
        Task<T> GetByIdAsync(string url, string id);
        Task<T> DeleteAsync(string url, string id);
    }
}
