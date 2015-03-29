using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using OwinIdentityAuthentication.Identity;

namespace OwinIdentityAuthentication.Authorization
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //Responsible for validating client
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // This call is required...
            // but we're not using client authentication, so validate and move on...
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // DEMO ONLY: Pretend we are doing some sort of REAL checking here:
            if (context.Password != "password")
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }

            using (var signInManager = context.OwinContext.Get<ApplicationSignInManager>())
            {
                using (var roleManager = context.OwinContext.Get<ApplicationRoleManager>())
                {
                    
                }
            }

            // Create or retrieve a ClaimsIdentity to represent the 
            // Authenticated user:
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("user_name", context.UserName));

            //DEMO ONLY: Add a role claim
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            identity.AddClaim(new Claim("Action", "Resource1"));

            // Identity info will ultimately be encoded into an Access Token
            // as a result of this call:
            await Task.FromResult(context.Validated(identity));
        }
    }
}
