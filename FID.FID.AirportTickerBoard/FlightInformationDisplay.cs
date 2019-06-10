using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FID.AirportTickerBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("FLIGHT INFORMATION DISPLAY:");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54358/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HTTP GET
                    var responseTask = client.GetAsync("v1/fid/FlightDetail");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<List<FlightDetail>>();
                        readTask.Wait();

                        var res = readTask.Result;

                        foreach (var item in res)
                        {
                            Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}", item.FlightNumber, item.AirlineName, item.Destination, item.Flight_Status, item.Actual_Departure_Time);
                        }
                    }
                }
                
                Console.ReadKey();
                Console.Clear();
            }
        }

      

    }
}
