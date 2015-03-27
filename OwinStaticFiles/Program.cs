using System;

namespace OwinFileServer
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
                Console.WriteLine("Server running at {0} - press any key to quit. ", baseUri);
                Console.ReadLine();
            }
        }
    }
}
