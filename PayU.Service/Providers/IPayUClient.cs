using System.Threading.Tasks;
using PayU.Model;

namespace PayU.Service.Providers
{
    public interface IPayUClient
    {
        Task<string> MakeOrder(PayUOrder order);
    }
}