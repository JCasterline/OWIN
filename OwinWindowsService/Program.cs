using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OwinWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            string baseUri = "http://localhost:8080";
            var svc = new Service();
            Console.WriteLine("Starting service...");
            svc.InternalStart();
            Console.WriteLine("Web server running at {0}.", baseUri);
            Console.WriteLine("Press any key to stop service...");
            Console.ReadLine();
            svc.InternalStop();
            Console.WriteLine("Stopping service...");
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service() 
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
