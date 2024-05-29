﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Services
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } =string.Empty;
        [Required]
        public double Price { get; set; } 

       
    }
}
 