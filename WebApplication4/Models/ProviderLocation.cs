using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ProviderLocation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public DateTime SubmittedAt { get; set; }
    }
}
