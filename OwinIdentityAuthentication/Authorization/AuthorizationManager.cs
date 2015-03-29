using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using OwinIdentityAuthentication.ResourceAuthorization;

namespace OwinIdentityAuthentication.Authorization
{
    public class AuthorizationManager : ResourceAuthorizationManager
    {
        //Used when checking for access using ResourceAuthorize and Request.CheckAccessAsync
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            return Task.FromResult(CheckAccess(context));
        }

        public static bool CheckAccess(string resource, string[] action)
        {
            return CheckAccess(resource, action, Thread.CurrentPrincipal as ClaimsPrincipal);
        }

        public static bool CheckAccess(string resource, string[] action, ClaimsPrincipal principal)
        {
            var context = new ResourceAuthorizationContext(principal, resource, action);

            return CheckAccess(context);
        }

        public static bool CheckAccess(ResourceAuthorizationContext context)
        {
            var hasAccess = context.Principal.Claims.Where(x => x.Type == context.Action.First().Value)
                .Select(x => x.Value)
                .Intersect(context.Resource.Select(x => x.Value))
                .Any();

            return hasAccess;
        }
    }
}
