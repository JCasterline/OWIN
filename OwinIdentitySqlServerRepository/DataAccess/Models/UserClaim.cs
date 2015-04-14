using RepositoryPattern;

namespace OwinIdentitySqlServerRepository.DataAccess.Models
{
    public class UserClaim : IEntity<int>
    {
        private const string _tableName = "UserClaims";
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
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}