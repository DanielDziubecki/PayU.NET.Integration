using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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

            var baseAddress = new Uri("https://private-anon-939139d112-payu21.apiary-mock.com/");
            using (var httpClient = new HttpClient {BaseAddress = baseAddress})
            {
                using (var content = new StringContent(
                    $"grant_type=trusted_merchant&client_id={clientId}&client_secret={secret}",
                    System.Text.Encoding.Default,
                    "application/x-www-form-urlencoded"))
                {
                    using (var response = await httpClient.PostAsync("pl/standard/user/oauth/authorize", content))
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
            var baseAddress = new Uri("https://private-anon-939139d112-payu21.apiary-mock.com/");
            var token = await GetAccessToken();
            var jsonOrder = new JavaScriptSerializer().Serialize(order);
            using (var httpClient = new HttpClient {BaseAddress = baseAddress})
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var content = new StringContent(
                    jsonOrder,
                    System.Text.Encoding.Default,
                    "application/json"))
                {
                    using (var response = await httpClient.PostAsync("api/v2_1/orders", content))
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                    }
                }
            }

            return null;
        }
    }
}