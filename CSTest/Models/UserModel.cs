using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CSTest.Models
{
    public class UserModel // : IdentityUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
