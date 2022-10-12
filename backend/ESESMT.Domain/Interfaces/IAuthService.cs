using System.Threading.Tasks;

namespace ESESMT.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> ValidateCredentials(string username, string password);
    }
}
