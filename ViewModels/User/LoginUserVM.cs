using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace eBusiness.ViewModels
{
    public class LoginUserVM
    {
        [Required]
        public string UsernameorEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistance { get; set; } = false;
    }
}
