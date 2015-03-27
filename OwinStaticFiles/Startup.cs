using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using OwinFileServer;

[assembly: OwinStartup(typeof(Startup))]

namespace OwinFileServer
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

            //app.Run(context =>
            //{
            //    context.Response.ContentType = "text/plain";
            //    return context.Response.WriteAsync("Hello World");
            //});
        }
    }
}