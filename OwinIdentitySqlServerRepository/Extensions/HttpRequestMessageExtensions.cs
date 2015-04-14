using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using OwinIdentitySqlServerRepository.ResourceAuthorization;

namespace OwinIdentitySqlServerRepository.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static bool CheckAccess(this HttpRequestMessage request, string action, params string[] resources)
        {
            return Task.Run(()=>request.CheckAccessAsync(action, resources)).Result;
        }

        public static Task<bool> CheckAccessAsync(this HttpRequestMessage request, string action, params string[] resources)
        {
            var user = request.GetRequestContext().Principal as ClaimsPrincipal;
            user = user ?? new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>{new Claim(ClaimTypes.Name, "")}));

            var ctx = new ResourceAuthorizationContext(user, action, resources);

            return request.CheckAccessAsync(ctx);
        }

        public static Task<bool> CheckAccessAsync(this HttpRequestMessage request, IEnumerable<Claim> actions, IEnumerable<Claim> resources)
        {
            var authorizationContext = new ResourceAuthorizationContext(
                request.GetOwinContext().Authentication.User ?? new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>{new Claim(ClaimTypes.Name, "")})),
                actions,
                resources);

            return request.CheckAccessAsync(authorizationContext);
        }

        public static Task<bool> CheckAccessAsync(this HttpRequestMessage request, ResourceAuthorizationContext authorizationContext)
        {
            return request.GetOwinContext().CheckAccessAsync(authorizationContext);
        }

        private static async Task<bool> CheckAccessAsync(this IOwinContext context, ResourceAuthorizationContext authorizationContext)
        {
            return await context.GetAuthorizationManager().CheckAccessAsync(authorizationContext);
        }

        private static IResourceAuthorizationManager GetAuthorizationManager(this IOwinContext context)
        {
            var am = context.Get<IResourceAuthorizationManager>(ResourceAuthorizationManagerMiddleware.Key);

            if (am == null)
            {
                throw new InvalidOperationException("No AuthorizationManager set.");
            }

            return am;
        }
    }
}
