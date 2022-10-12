using System.Threading.Tasks;

namespace ESESMT.Infra.Shared.Interfaces
{
    public interface IHttpRequests
    {
        Task<string> GetAsync(string endpoint);
        Task<string> DeleteAsync(string endpoint);
        Task<string> PostAsync(string endpoint, object objectToSend);
    }
}
