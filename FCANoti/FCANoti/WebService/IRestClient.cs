using System;
using System.Text;
using System.Threading.Tasks;

namespace FCANoti.WebService
{
    public interface IRestClient<T, R> : IDisposable
    {
        Task<R> Post(T parameterModel, string apiRoute, MediaType mediaType, Encoding encoding);
        Task<R> Get(string apiRoute, MediaType mediaType);
        Task<R> Put(T parameter);
        Task<R> Delete(T parameter);
    }
}
