using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OwinIdentitySqlServerRepository.DataAccess;
using OwinIdentitySqlServerRepository.DataAccess.Extensions;
using OwinIdentitySqlServerRepository.DataAccess.Models;
using Claim = System.Security.Claims.Claim;

namespace OwinIdentitySqlServerRepository.Identity
{
    public class AppUserStore :
        IUserLoginStore<User, int>,
        IUserRoleStore<User, int>,
        IUserClaimStore<User, int>,
        IUserPasswordStore<User, int>,
        IUserEmailStore<User, int>,
        IUserPhoneNumberStore<User, int>,
        IUserLockoutStore<User, int>,
        IUserSecurityStampStore<User, int>,
        IUserTwoFactorStore<User, int>
    {
        public SqlConnection Connection { get; private set; }

        public AppUserStore(SqlConnection connection)
        {
            Connection = connection;
        }

        public Task AddClaimAsync(User user, Claim claim)
        {
            var userClaim = new {UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value};
            return Task.Run(() => { Connection.Insert(userClaim, new UserClaim().Destination); });
        }

        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            return Task.Run(() =>
            {
                var results = Connection.Select<UserClaim>(new {UserId = user.Id}, new UserClaim().Destination);
                return (IList<Claim>)results.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList();
            });
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            var userClaim = new {UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value};
            return Task.Run(()=>Connection.Delete(userClaim, new UserClaim().Destination));
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return Task.Run(()=>Connection.Select<User>(new {Email=email}, new User().Destination).FirstOrDefault());
        }

        public Task<string> GetEmailAsync(User user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailAsync(User user, string email)
        {
            return Task.FromResult(user.Email = email);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            return Task.FromResult(user.EmailConfirmed = confirmed);
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(user.IsLockedOut);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(user.LockoutEndDateTimeOffset);
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(++user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(user.AccessFailedCount = 0);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(user.IsLockedOut = enabled);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult(user.LockoutEndDateTimeOffset = lockoutEnd);
        }

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            var userLogin = new
            {
                UserId = user.Id,
                login.LoginProvider,
                login.ProviderKey
            };

            return Task.Run(() => { Connection.Insert(userLogin, new UserLogin().Destination); });
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            return Task.Run(() => Connection.ExecuteQuery<User>(Statements.FindUserByLoginInfo, login.LoginProvider.AsParameter("@loginprovider"), login.ProviderKey.AsParameter("@providerkey")).FirstOrDefault());
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            return Task.Run(() => (IList<UserLoginInfo>)Connection.Select<UserLoginInfo>(new{UserId=user.Id}, new UserLogin().Destination).ToList());
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            var userLogin = new
            {
                login.LoginProvider,
                login.ProviderKey
            };

            return Task.Run(() => Connection.Delete(userLogin, new UserLogin().Destination));
        }

        public Task CreateAsync(User user)
        {
            return Task.Run(() => Connection.Insert(user, user.Destination));
        }

        public Task DeleteAsync(User user)
        {
            return Task.Run(() => Connection.Delete(new{user.Id}, user.Destination));
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task.Run(() => Connection.Select<User>(new{Id=userId}, new User().Destination).FirstOrDefault());
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.Run(() => Connection.Select<User>(new{UserName=userName}, new User().Destination).FirstOrDefault());
        }

        public Task UpdateAsync(User user)
        {
            return Task.Run(() => Connection.Insert(user, user.Destination));
        }

        public void Dispose()
        {

        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.FromResult(user.PasswordHash = passwordHash);
        }

        public Task<string> GetPhoneNumberAsync(User user)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber)
        {
            return Task.FromResult(user.PhoneNumber = phoneNumber);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed)
        {
            return Task.FromResult(user.PhoneNumberConfirmed = confirmed);
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.FromResult((IList<string>) new List<string>());
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.FromResult(false);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(User user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            return Task.FromResult(user.SecurityStamp = stamp);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(user.TwoFactorEnabled = enabled);
        }
    }
}