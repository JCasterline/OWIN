using System.Threading.Tasks;

namespace OwinIdentityAuthentication.ResourceAuthorization
{
    public interface IResourceAuthorizationManager
    {
        Task<bool> CheckAccessAsync(ResourceAuthorizationContext context);
    }
}
