using System.Threading.Tasks;

namespace PayU.Client.Services
{
    public interface IPaymentService
    {
        Task PayForOrder(OrderDto order,string token);
    }
}