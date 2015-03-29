using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OwinIdentityAuthentication.Models;

namespace OwinIdentityAuthentication.Extensions
{
    public static class UserExtensions
    {
        static public async Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User, int> manager,
             string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(user, authenticationType);

            // Add custom user claims here

            //userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            //userIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            return userIdentity;
        }
    }
}
