using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Service { get; set; }
        public string? Description { get; set; }
        public DateTime SubmittedAt { get; set; }


    }
}
