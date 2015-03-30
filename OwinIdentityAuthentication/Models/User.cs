using Microsoft.AspNet.Identity;

namespace OwinIdentityAuthentication.Models
{
    public class User : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsDeleted { get; set; }
    }
}
