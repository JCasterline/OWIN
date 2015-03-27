using System;
using System.CodeDom;
using Microsoft.Owin;
using Owin;
using OwinConsoleApp;

[assembly: OwinStartup(typeof(Startup))]

namespace OwinConsoleApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello World");
            });
        }
    }
}
