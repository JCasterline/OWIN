using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

//2) The following attribute will set the startup class to the 'Startup' class in the OwinStartupClassDetection namespace.
//Default startup class.
[assembly: OwinStartup(typeof(OwinStartupClassDetection.Startup))]

//Named startup classes that can be referenced in the configuration file.
[assembly: OwinStartup("ProductionConfiguration", typeof(OwinStartupClassDetection.ProductionStartup))]
[assembly: OwinStartup("ProductionConfiguration2", typeof(OwinStartupClassDetection.ProductionStartup2))]

namespace OwinStartupClassDetection
{
    //1) Katana looks for a class named Startup in the namespace matching the assembly name or the global namespace.
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
    public class ProductionStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                string t = DateTime.Now.Millisecond.ToString();
                return context.Response.WriteAsync(t + " Production OWIN App");
            });
        }
    }
    public class ProductionStartup2
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                string t = DateTime.Now.Millisecond.ToString();
                return context.Response.WriteAsync(t + " 2nd Production OWIN App");
            });
        }
    }
}

//3) The appSetting element overrides the OwinStart attribute and naming convention. You can have 
//    multiple startup classes (each using an OwinStartup attribute) and configure which startup 
//    class will be loaded in a configuration file.