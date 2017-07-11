using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PayU.Client.Services;


namespace PayU.Client.Controllers
{
    [Authorize(Roles = "Manager")]
    public class OrderController : Controller
    {
        private readonly IPaymentService paymentService;

        public OrderController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
       
        public async Task<ActionResult> MakeOrder(OrderDto order)
        {
            var token = HttpContext.Request.Cookies.Get("token");

            if (token != null)
            {
                var redirect = (await paymentService.GetRedirectUrl(order, token.Value)).Replace("\"", "");

                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new Dictionary<string, string> { { "url", redirect } }
                };
            }
            return null;

        }

        public ActionResult OrderMaked()
        {
            return View("OrderMaked");
        }
    }
}