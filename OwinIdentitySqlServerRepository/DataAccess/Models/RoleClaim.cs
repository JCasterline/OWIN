using RepositoryPattern;

namespace OwinIdentitySqlServerRepository.DataAccess.Models
{
    public class RoleClaim : IEntity<int>
    {
        private const string _tableName = "RoleClaims";
        public string Destination
        {
            get { return _tableName; }
        }

        public string Source
        {
            get { return _tableName; }
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ClaimId { get; set; }
    }
}