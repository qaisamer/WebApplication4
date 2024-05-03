using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string FristName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
