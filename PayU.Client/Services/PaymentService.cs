
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Script.Serialization;
using PayU.Model;

namespace PayU.Client.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IProductService productService;

        public PaymentService(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task PayForOrder(OrderDto order,string token)
        {
            var product = productService.GetProductById(Guid.Parse(order.ProductId));
            var payUProducts = new List<PayUProduct>
            {
                new PayUProduct
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = order.Quantity.ToString()
                }
            };
            var totalAmount = payUProducts.Sum(x=>double.Parse(x.Price)*int.Parse(x.Quantity)).ToString();
            var payUOrder = new PayUOrder
            {
                ContinueUrl = "http://localhost:51403/Order/OrderMaked",
                CurrencyCode = "PLN",
                CustomerIp = "10.10.1.1",
                Description = "Super",
                MerchantPosId = "301562",
                NotifyUrl = "http://localhost:51369/notify",
                Products = payUProducts,
                TotalAmount = totalAmount
            };

            var serializedOrder = new JavaScriptSerializer().Serialize(payUOrder);

            using (var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:51369/") })
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var content = new StringContent(
                    serializedOrder,
                    System.Text.Encoding.Default,
                    "application/json"))
                {
                    using (var response = await httpClient.PostAsync("Order", content))
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                    }
                }
            }
        }
    }
}