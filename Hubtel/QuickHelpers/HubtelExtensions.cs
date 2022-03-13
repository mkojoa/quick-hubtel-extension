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
        public static string ClientId = "";
        public static string Secret = "";
        public static string From = "Panda"; // not in use;
        public static string BulkSMSUrl = "https://smsc.hubtel.com/v1/messages";


        /// <summary>
        /// Api Call
        /// </summary>
        /// <returns></returns>
        public static async Task<string> PrepareSmsApiCall()
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
