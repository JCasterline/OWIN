using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OwinIdentitySqlServerRepository.DataAccess.Models;

namespace OwinIdentitySqlServerRepository.Identity
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public new AppUserStore Store
        {
            get { return (AppUserStore)base.Store; }
        }

        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IUserStore<User, int> userStore)
        {
            var manager = new ApplicationUserManager(userStore);

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            manager.EmailService = new EmailService();
            // Configure user lockout defaults
            //manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                //Token provider that generates encrypted time based codes using the user security stamp.
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromHours(3)
                    };
            }
            else
            {
                //Token provider that generates time based codes using the user security stamp.
                manager.UserTokenProvider = new TotpSecurityStampBasedTokenProvider<User, int>();
            }

            return manager;
        }
    }
}
