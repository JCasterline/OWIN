using System.Threading.Tasks;
using OwinClaimAuthorization.Authorization;

namespace OwinClaimAuthorization.ResourceAuthorization
{
    public interface IResourceAuthorizationManager
    {
        Task<bool> CheckAccessAsync(ResourceAuthorizationContext context);
    }
}
