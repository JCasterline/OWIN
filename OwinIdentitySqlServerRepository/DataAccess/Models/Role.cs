using Microsoft.AspNet.Identity;
using RepositoryPattern;

namespace OwinIdentitySqlServerRepository.DataAccess.Models
{
    public class Role : IRole<int>, IEntity<int>
    {
        private const string _tableName = "Roles";

        public string Destination
        {
            get { return _tableName; }
        }

        public string Source
        {
            get { return _tableName; }
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
