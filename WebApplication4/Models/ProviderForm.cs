using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ProviderForm
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string SecondName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public int WorkExp { get; set;}
        [Required]
        public string Gender { get; set;} = string.Empty;

    }
}
