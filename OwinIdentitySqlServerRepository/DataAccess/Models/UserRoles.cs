using RepositoryPattern;

namespace OwinIdentitySqlServerRepository.DataAccess.Models
{
    public class UserRole : IEntity<int>
    {
        private const string _tableName = "UserRoles";
        public string Destination
        {
            get { return _tableName; }
        }

        public string Source
        {
            get { return _tableName; }
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}