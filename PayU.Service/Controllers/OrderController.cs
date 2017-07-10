using System.Threading.Tasks;
using System.Web.Http;
using PayU.Model;

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
            var redirect = await payUClient.MakeOrder(order);
            return redirect;
        }

        [HttpPost]
        [Route("notify")]
        public async Task<string> Notify(dynamic notify)
        {
           
            return "";
        }

    }
}