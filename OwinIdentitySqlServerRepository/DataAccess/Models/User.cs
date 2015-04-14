using System;
using Microsoft.AspNet.Identity;
using RepositoryPattern;

namespace OwinIdentitySqlServerRepository.DataAccess.Models
{
    public class User : IUser<int>, IEntity<int>
    {
        private const string _tableName = "Users";

        public string Destination
        {
            get { return _tableName; }
        }

        public string Source
        {
            get { return _tableName; }
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTimeOffset LockoutEndDateTimeOffset { get; set; }
        public bool IsDeleted { get; set; }
    }
}