﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace OwinSelfHostedWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.

            // Specify the URI to use for the local host:
            const string baseUri = "http://localhost:8080";

            Console.WriteLine("Starting web Server...");
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(baseUri))
            {
                Console.WriteLine("Server running at {0}.", baseUri);
                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();

                var response = client.GetAsync(baseUri + "api/values").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine("Press any key to quit...");
                Console.ReadLine();
            }
        }
    }
}
