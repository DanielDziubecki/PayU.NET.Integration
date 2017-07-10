using System.Threading.Tasks;

namespace PayU.Client.Services
{
    public interface IPaymentService
    {
        Task<string> PayForOrder(OrderDto order,string token);
    }
}