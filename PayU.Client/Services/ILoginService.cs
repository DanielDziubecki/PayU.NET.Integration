using System.Threading.Tasks;

namespace PayU.Client.Services
{
    public interface ILoginService
    {
        Task<string> GetToken(LoginDto loginDto);
    }
}