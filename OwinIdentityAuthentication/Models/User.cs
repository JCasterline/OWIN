using Microsoft.AspNet.Identity;

namespace OwinIdentityAuthentication.Models
{
    public class User : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
