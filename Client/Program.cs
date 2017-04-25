using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            // List all products.
            HttpResponseMessage response = client.GetAsync("api/product").Result;

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                foreach (var p in products)
                {
                    Console.WriteLine("{0}\t{1};\t{2}", p.ProductID, p.ProductName, p.Quantity);
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

        }
    }
}
