﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OwinIdentityAuthentication.Models;

namespace OwinIdentityAuthentication.Identity
{
    public class AppRoleStore: IRoleStore<Role, int>
    {
        public Task CreateAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
