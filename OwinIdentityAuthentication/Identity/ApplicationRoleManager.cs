using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OwinIdentityAuthentication.Models;

namespace OwinIdentityAuthentication.Identity
{
    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        public ApplicationRoleManager(IRoleStore<Role, int> store)
            : base(store)
        {

        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
            IRoleStore<Role, int> roleStore)
        {
            var manager = new ApplicationRoleManager(roleStore);

            return manager;
        }
    }
}
