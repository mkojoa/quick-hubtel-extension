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
            try
            {
                // extension to call
                HubtelExtensions.From = "Panda";

                //var result1 = await HubtelExtensions.ApiCall1();
                var result2 = await HubtelExtensions.ApiCall2();

                //Console.WriteLine(result1);
                Console.WriteLine(result2);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

 

