using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Hubtel.QuickHelpers
{
    public static class HubtelExtensions
    {
        // Constant
        private static string ClientId = "ryxqkwnm";
        private static string Secret = "vjbipotk";
        public static string From = "Panda"; // not in use;
        private static string BulkSMSUrl = "https://smsc.hubtel.com/v1/messages";


        /// <summary>
        /// Api Call I
        /// </summary>
        /// <returns></returns>
        public static async Task<string> ApiCall1()
        {
            var apiUrl = BulkSMSUrl;
            var apiClient = ClientId;
            var apiSecret = Secret;

            var data = GetCustomerDetailsFromDb();

            var msg = $"Dear {data.Name}, " +
                         $"\r\n" +
                         $"Your reset password code is : {data.Code}. \r\n" +
                         $"Don't share this code with others" +
                         $"\r\n" +
                         $"Panda";


            var urlConst = $"{apiUrl}/send?From={data.From}&To={data.Phone}&Content={msg}&ClientID={apiClient}&ClientSecret={apiSecret}&RegisteredDelivery=true&FromToContentClientIdClientSecretRegisteredDelivery=";

            HttpClient _client = new();

            HttpResponseMessage resp = await _client.GetAsync(urlConst);

            string response;
            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsStringAsync();

                response = $"{result}";
            }
            else
            {
                response = $"Please try again...";
            }

            return response;
        }
         
        /// <summary>
        ///  Api Call II
        /// </summary>
        /// <returns></returns>
        public static async Task<string> ApiCall2()
        {
            var data = GetCustomerDetailsFromDb();

            var InputForm = new
            {
                from = "MySenderId",
                to = "0553771219",
                content = $"Dear {data.Name}, " +
                          $"\r\n" +
                          $"Your reset password code is : {data.Code}. \r\n" +
                          $"Don't share this code with others" +
                          $"\r\n" +
                          $"Panda"
            };
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(InputForm);

            using (var client = new HttpClient())
            {

                var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ClientId}:{Secret}"));

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64String);

                JObject json = JObject.Parse(msg);

                var postData = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var resp = await client.PostAsync(
                    $"{BulkSMSUrl}/send", 
                    postData
                );

                string response;
                if (resp.IsSuccessStatusCode)
                {
                    var result = await resp.Content.ReadAsStringAsync();

                    response = $"{result}";
                }
                else
                {
                    response = $"Please try again...";
                }

                return response;

            }
        }

        /// <summary>
        /// Customer Data
        /// </summary>
        /// <returns></returns>
        private static HubtelDto GetCustomerDetailsFromDb()
            => new()
            {
                Name = "Michael",
                Phone = "0553771219",
                From = "0276002658",
                Code = Guid.NewGuid().ToString(), // change later
            };
    }
}
