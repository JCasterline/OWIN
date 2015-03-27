using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace OwinTokenAuthentication
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            ConfigureAuth(app);

            // Configure Web API for self-host. 
            app.UseWebApi(ConfigureWebApi()); 
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider =  new OAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),

                //True to allow authorize and token requests to arrive on http URI addresses, and to allow incoming redirect_uri authorize request parameter to have http URI addresses.
                //Only set to true for demo purposes
                //In production you want to connect to the authorization server using a secure SSL/TLS protocol (HTTPS), since you are transporting user credentials.
                AllowInsecureHttp = true,
            };
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
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
