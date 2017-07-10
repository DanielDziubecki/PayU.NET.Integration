using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayU.Model;

namespace PayU.Service
{
    public interface IPayUClient
    {
        Task<string> MakeOrder(PayUOrder order);
    }

    public class PayUClient : IPayUClient
    {
        private async Task<string> GetAccessToken()
        {
            var clientId = System.Configuration.ConfigurationManager.AppSettings["client_id"];
            var secret = System.Configuration.ConfigurationManager.AppSettings["client_secret"];

            var baseAddress = new Uri("https://secure.snd.payu.com/");
            using (var httpClient = new HttpClient {BaseAddress = baseAddress})
            {
                using (var content = new StringContent(
                    $"grant_type=client_credentials&client_id={clientId}&client_secret={secret}",
                    System.Text.Encoding.Default,
                    "application/x-www-form-urlencoded"))
                {
                    using (var response = await httpClient.PostAsync("/pl/standard/user/oauth/authorize", content))
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var deserializedResponse =
                                new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(responseData);

                            var token = deserializedResponse["access_token"];

                            return token;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<string> MakeOrder(PayUOrder order)
        {
            var token = await GetAccessToken();
            var jsonOrder = JsonConvert.SerializeObject(order, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var baseAddress = new Uri("https://secure.snd.payu.com/api/v2_1/orders");

         //   var testContent = File.ReadAllText(@"C:\Users\Daniel\Desktop\git hub pet projects\PayU.NET.Integration\PayU.Service\testOrder.json");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var content = new StringContent(jsonOrder, System.Text.Encoding.Default, "application/json"))
                {
                    using (var response = await httpClient.PostAsync(baseAddress,content))
                    {
                        if (response.StatusCode == HttpStatusCode.Redirect ||
                            response.StatusCode == HttpStatusCode.MovedPermanently)
                        {
                          return response.Headers.Location.AbsoluteUri;
                        }
                    }
                }
            }

            return null;
        }
    }
}