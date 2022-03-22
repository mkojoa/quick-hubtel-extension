using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;
using Hubtel.QuickHelpers;
using System.Net;
using System.IO;

namespace Hubtel
{
    internal class Program
    {

        private static string callUrl;


        public static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

       public static void Main(string[] args)
        {
            try
            {
                // extension to call
                HubtelExtensions.From = "Panda";
                             

                string mobile = "PHONENUM";
                string msgBody = "Your sms body here";
                
                string clientId = "clientID";
                string clientSecret = "clientSecret";




                msgBody = msgBody.Replace(' ', '+');

                callUrl = @"https://api.hubtel.com/v1/messages/send?From=PANDA&To=" + mobile + "&Content=" + msgBody + "&ClientId=" + clientId + "&ClientSecret=" + clientSecret + "&RegisteredDelivery=true";
                
                string msgReply = Get(callUrl);

                if (msgReply.Contains("\"Status\": 0, "))
                {
                    Console.WriteLine("SMS Sent");
                    
                    //include audit if applicable
                }
                else
                {
                    //Exception Handling + audit
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

 

