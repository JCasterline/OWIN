using System;
using System.Collections.Generic;

namespace OwinIdentityAuthentication.ResourceAuthorization
{
    public class ResourceAuthorizationMiddlewareOptions
    {
        public ResourceAuthorizationMiddlewareOptions()
        {
            ManagerProvider = (env) => null;
        }
        public IResourceAuthorizationManager Manager { get; set; }
        public Func<IDictionary<string, object>, IResourceAuthorizationManager> ManagerProvider { get; set; }
    }
}
