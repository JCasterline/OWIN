using RepositoryPattern;

namespace OwinIdentitySqlServerRepository.DataAccess.Models
{
    public class Claim : IEntity<int>
    {
        private const string _tableName = "Claims";

        public string Destination
        {
            get { return _tableName; }
        }

        public string Source
        {
            get { return _tableName; }
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}