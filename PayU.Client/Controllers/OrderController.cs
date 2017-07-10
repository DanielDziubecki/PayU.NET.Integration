using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PayU.Client.Services;


namespace PayU.Client.Controllers
{
   [Authorize]
    public class OrderController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;

      
        public OrderController(IPaymentService paymentService , IMapper mapper)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
        }
       
        public async Task<ActionResult> MakeOrder(OrderDto order)
        {
            var token = HttpContext.Request.Cookies.Get("token");

            if (token != null)
            {
                var redirect = await  paymentService.PayForOrder(order, token.Value);
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