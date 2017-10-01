using System.Threading.Tasks;
using System.Web.Http;
using PayU.Model;
using PayU.Service.Providers;

namespace PayU.Service.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        private readonly IPayUClient payUClient;

        public OrderController(IPayUClient payUClient)
        {
            this.payUClient = payUClient;
        }

        [HttpPost]
        [Route("order")]
        public async Task<string> MakeOrder(PayUOrder order)
        {
            var orderUrl = await payUClient.MakeOrder(order);
            return orderUrl;
        }
    }
}