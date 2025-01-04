using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Locator
{
    public class Data
    { 
      public string city {  get; set; }
      public string region { get; set; }
      public string country { get; set; }
      public string loc { get; set; }
      public string org { get; set; }
      public string postal { get; set; }
      public string timezone { get; set; }

    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Locator";
            Console.Write("Enter IP address : ");
            string ip = Console.ReadLine();
            string url = $"https://ipinfo.io/{ip}/json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine("[+] request Successfully Made");

                    string responseData = await response.Content.ReadAsStringAsync();
                    Data ipinfo = JsonConvert.DeserializeObject<Data>(responseData);

                    Console.Clear();
                    Console.WriteLine($"Country : {ipinfo.country}");
                    Console.WriteLine($"City : {ipinfo.city}");
                    Console.WriteLine($"Coordinates : {ipinfo.loc}");
                    Console.WriteLine($"Postal Code : {ipinfo.postal}");
                    Console.WriteLine($"Region  : {ipinfo.region}");
                    Console.WriteLine($"ASN : {ipinfo.org}");
                    string[] Coords = ipinfo.loc.Split(',');
                    Console.WriteLine($"Google Maps :https://www.google.com/maps/?q={Coords[0]},{Coords[1]}");

                }
                catch(HttpRequestException ex) {

                    Console.WriteLine($"Error : {ex.Message}");

                }

            }

        }
    }
}

