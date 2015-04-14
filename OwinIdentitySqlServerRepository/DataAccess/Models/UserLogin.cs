using RepositoryPattern;

namespace OwinIdentitySqlServerRepository.DataAccess.Models
{
    public class UserLogin : IEntity<int>
    {
        private const string _tableName = "UserLogins";

        public string Destination
        {
            get { return _tableName; }
        }

        public string Source
        {
            get { return _tableName; }
        }

        public int Id{get; set; }
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
