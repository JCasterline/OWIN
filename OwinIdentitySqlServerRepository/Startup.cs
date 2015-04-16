using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using OwinIdentitySqlServerRepository.Authorization;
using OwinIdentitySqlServerRepository.DataAccess;
using OwinIdentitySqlServerRepository.Extensions;
using OwinIdentitySqlServerRepository.Identity;

namespace OwinIdentitySqlServerRepository
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ILoggerFactory factory = new CustomOwinLoggerFactory(new LoggerFactory.LoggerFactory());
            app.SetLoggerFactory(factory);

            ConfigureAuth(app);

            //Adds middleware for providing a resource/action based authorization manager.
            app.UseResourceAuthorization(new AuthorizationManager());

            // Configure Web API for self-host. 
            app.UseWebApi(ConfigureWebApi()); 
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            //Set protection provider used to encrypt user tokens
            //Comment this out out create time based non encrypted codes
            //appBuilder.SetDataProtectionProvider(new DpapiDataProtectionProvider());

            app.CreatePerOwinContext<ApplicationUserManager>(
                (options, context) =>
                    ApplicationUserManager.Create(options, new AppUserStore(new SqlServerDatabase().CreateConnection())));

            app.CreatePerOwinContext<ApplicationRoleManager>(
                (options, context) =>
                    ApplicationRoleManager.Create(new AppRoleStore(new SqlServerDatabase().CreateConnection())));

            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider =  new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),

                //True to allow authorize and token requests to arrive on http URI addresses, and to allow incoming redirect_uri authorize request parameter to have http URI addresses.
                //Only set to true for demo purposes
                //In production you want to connect to the authorization server using a secure SSL/TLS protocol (HTTPS), since you are transporting user credentials.
                AllowInsecureHttp = true,
            };
            app.UseOAuthAuthorizationServer(oAuthOptions);
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

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return config;
        }
    }
}
