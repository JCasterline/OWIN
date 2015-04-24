using Microsoft.AspNet.Identity;
using OwinIdentitySqlServerRepository.DataAccess.Models;

namespace OwinIdentitySqlServerRepository.Identity
{
    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        public new AppRoleStore Store
        {
            get { return (AppRoleStore)base.Store; }
        }

        public ApplicationRoleManager(IRoleStore<Role, int> store)
            : base(store)
        {

        }

        public static ApplicationRoleManager Create(IRoleStore<Role, int> roleStore)
        {
            var manager = new ApplicationRoleManager(roleStore);

            return manager;
        }
    }
}
