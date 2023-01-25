using Microsoft.AspNetCore.Identity;
using System.Configuration;

namespace eBusiness.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
