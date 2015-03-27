using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfHostedSignalR.Hubs
{
    public class MyHub : Microsoft.AspNet.SignalR.Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
    }
}
