using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using OwinIdentitySqlServerRepository.Identity;

namespace OwinIdentitySqlServerRepository.Authorization
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
            using (var signInManager = context.OwinContext.Get<ApplicationSignInManager>())
            {
                //Set authentication type from the context authentication type. This is for CreateUserIdentityAsync so the authentication type is not hardcoded in that method
                signInManager.AuthenticationType = context.Options.AuthenticationType;
                var result = await signInManager.PasswordSignInAsync(context.UserName, context.Password, false, true);
                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            var user = signInManager.UserManager.FindByName(context.UserName);
                            if (user.IsLockedOut)
                            {
                                context.SetError("invalid_grant", "The user name has been locked out.");
                                break;
                            }
                            if (user.IsDeleted)
                            {
                                context.SetError("invalid_grant", "The user name has been deleted.");
                                break;
                            }
                            //add claims to identity here
                            var identity = await signInManager.CreateUserIdentityAsync(user);
                            var ticket = new AuthenticationTicket(identity, CreateProperties(identity));
                            context.Validated(ticket);
                            break;
                        }
                    case SignInStatus.Failure:
                        {
                            context.SetError("invalid_grant", "The user name or password is incorrect.");
                            break;
                        }
                    case SignInStatus.LockedOut:
                        {
                            context.SetError("invalid_grant", "The user name has been locked out.");
                            break;
                        }
                    case SignInStatus.RequiresVerification:
                        {
                            context.SetError("invalid_grant", "The user name requires verfication.");
                            break;
                        }
                }
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
                context.AdditionalResponseParameters.Add(property.Key, property.Value);

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(ClaimsIdentity oAuthIdentity)
        {
            //Create list of claims to use in client
            //string permissions = oAuthIdentity.Claims

            return new AuthenticationProperties(
                new Dictionary<string, string>
                {
                    //{"permissions", permissions},
                    {"userName", oAuthIdentity.GetUserName()},
                    {"givenName", oAuthIdentity.FindFirstValue(ClaimTypes.GivenName)??string.Empty},
                    {"email", oAuthIdentity.FindFirstValue(ClaimTypes.Email)??string.Empty}
                });
        }
    }
}
