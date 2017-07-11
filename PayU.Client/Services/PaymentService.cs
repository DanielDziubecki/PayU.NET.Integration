
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        public async Task<string> GetRedirectUrl(OrderDto order,string token)
        {
            var payUOrder = PreparePayUOrder(order);

            var serializedOrder = JsonConvert.SerializeObject(payUOrder);

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
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
        }

        private PayUOrder PreparePayUOrder(OrderDto order)
        {
            var product = productService.GetProductById(Guid.Parse(order.ProductId));
            var payUProducts = new List<PayUProduct>
            {
                new PayUProduct
                {
                    Name = product.Name,
                    UnitPrice = product.Price,
                    Quantity = order.Quantity.ToString()
                }
            };
            var totalAmount = payUProducts.Sum(x => (int.Parse(x.UnitPrice) * int.Parse(x.Quantity)))
                .ToString(CultureInfo.InvariantCulture);

            var payUOrder = new PayUOrder
            {
                ContinueUrl = "http://localhost:51403/Order/OrderMaked",
                CurrencyCode = "PLN",
                CustomerIp = "127.0.0.1",
                Description = "Super",
                MerchantPosId = "301579",
                ExtOrderId = Guid.NewGuid().ToString("N"),
                NotifyUrl = "http://localhost:51369/notify",
                Products = payUProducts,
                TotalAmount = totalAmount,
                Buyer = new PayUBuyer
                {
                    Email = "test@wp.pl",
                    FirstName = "Daniel",
                    LastName = "Dziubecki",
                    Phone = "12332111"
                }
            };
            return payUOrder;
        }
    }
}