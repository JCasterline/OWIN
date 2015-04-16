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
    public class AppRoleStore :
        IRoleStore<Role, int>
    {
        public SqlConnection Connection { get; private set; }

        public AppRoleStore(SqlConnection connection)
        {
            Connection = connection;
        }

        public Task CreateAsync(Role role)
        {
            return Task.Run(() => Connection.Insert(new { role.Name }, role.Destination));
        }

        public Task DeleteAsync(Role role)
        {
            return Task.Run(() => Connection.Delete(new { role.Id }, role.Destination));
        }

        public Task<Role> FindByIdAsync(int roleId)
        {
            return Task.Run(() => Connection.Select<Role>(new { Id = roleId }, new Role().Destination).FirstOrDefault());
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Task.Run(() => Connection.Select<Role>(new { Name = roleName }, new Role().Destination).FirstOrDefault());
        }

        public Task UpdateAsync(Role role)
        {
            return Task.Run(() => Connection.Update(role, role.Destination));
        }

        public Task AddClaimAsync(Role role, Claim claim)
        {
            return
                Task.Run(
                    () =>
                        Connection.ExecuteNonQuery(Statements.AddRoleClaim, role.Id.AsParameter("@roleid"),
                            claim.Type.AsParameter("@type"), claim.Value.AsParameter("@value")));
        }
        public void Dispose()
        {

        }
    }
}