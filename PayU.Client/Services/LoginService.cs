using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PayU.Client.Services
{
    public class LoginService : ILoginService
    {
        public async Task<string> GetToken(LoginDto loginDto)
        {
            var tokenIssuer = ConfigurationManager.AppSettings["issuer"];
            using (var httpClient = new HttpClient {BaseAddress = new Uri($"{tokenIssuer}/oauth2/token")})
            {
                using (var response = await httpClient.PostAsync(httpClient.BaseAddress, new FormUrlEncodedContent(
                    new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username", loginDto.Username),
                        new KeyValuePair<string, string>("password", loginDto.Password),
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("client_id", ConfigurationManager.AppSettings["appId"])
                    })))
                {
                    var contents = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var deserializedResponse =
                            new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(contents);

                        var token = deserializedResponse["access_token"];

                        return token;
                    }
                }


                return null;
            }
        }
    }
}