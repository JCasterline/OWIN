using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Serialization;
using Owin;
using OwinFileServerWebApi;

[assembly: OwinStartup(typeof(Startup))]

namespace OwinFileServerWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.UseFileServer(new FileServerOptions()
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = new PhysicalFileSystem("webdir")
            });

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

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return config;
        }
    }
}