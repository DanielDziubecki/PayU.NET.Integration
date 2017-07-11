using System.Threading.Tasks;

namespace PayU.Client.Services
{
    public interface IPaymentService
    {
        Task<string> GetRedirectUrl(OrderDto order, string token);
    }
}