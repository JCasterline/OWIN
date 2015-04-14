using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OwinIdentitySqlServerRepository.DataAccess.Models;
using OwinIdentitySqlServerRepository.Identity;

namespace OwinIdentitySqlServerRepository.Extensions
{
    public static class UserExtensions
    {
        static public async Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User, int> manager,
             string authenticationType)
        {
            //var castMan = (ApplicationUserManager) manager;
            //var connection = castMan.Store.Connection;
            var userIdentity = await manager.CreateIdentityAsync(user, authenticationType);
            
            //userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            //userIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            return userIdentity;
        }
    }
}
