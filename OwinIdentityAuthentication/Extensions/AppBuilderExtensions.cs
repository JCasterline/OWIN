﻿using Owin;
using OwinIdentityAuthentication.ResourceAuthorization;

namespace OwinIdentityAuthentication.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseResourceAuthorization(this IAppBuilder app,
            IResourceAuthorizationManager authorizationManager)
        {
            ResourceAuthorizationMiddlewareOptions options = new ResourceAuthorizationMiddlewareOptions()
            {
                Manager = authorizationManager
            };
            UseResourceAuthorization(app, options);
            return app;
        }

        public static IAppBuilder UseResourceAuthorization(this IAppBuilder app,
            ResourceAuthorizationMiddlewareOptions options)
        {
            app.Use(typeof (ResourceAuthorizationManagerMiddleware), options);
            return app;
        }
    }
}