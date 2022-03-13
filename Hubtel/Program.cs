using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;
using Hubtel.QuickHelpers;

namespace Hubtel
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // extension to call
           var result =  await HubtelExtensions.PrepareSmsApiCall();

            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}

 

