using System.Threading.Tasks;

namespace OwinIdentitySqlServerRepository.ResourceAuthorization
{
    public interface IResourceAuthorizationManager
    {
        Task<bool> CheckAccessAsync(ResourceAuthorizationContext context);
    }
}
