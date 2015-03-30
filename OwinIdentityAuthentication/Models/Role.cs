using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace OwinIdentityAuthentication.Models
{
    public class Role : IRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Claim> Claims { get; set; } 
    }
}
