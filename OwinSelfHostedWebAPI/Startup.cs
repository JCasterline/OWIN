using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

namespace OwinSelfHostedWebAPI
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            // Configure Web API for self-host. 
            app.UseWebApi(ConfigureWebApi()); 
        }

        private static HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            return config;
        }
    }
}
