using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using PayU.Client.Filters;
using PayU.Client.Services;


namespace PayU.Client.Controllers
{
    [CustomAuthorize(Roles = "Janek")]
    public class OrderController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;

        public OrderController(IPaymentService paymentService , IMapper mapper)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
        }

        public ActionResult MakeOrder(OrderDto order)
        {

            //payment service here

            var url = Url.Action("OrderMaked");

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new Dictionary<string, string> { { "url", url } }
            };
      
        }

        public ActionResult OrderMaked()
        {
            return View("OrderMaked");
        }
    }
}