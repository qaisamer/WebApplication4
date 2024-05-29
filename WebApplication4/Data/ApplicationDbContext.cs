using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> appUsers { get; set; }
        public DbSet<Services>  services { get; set; }
        public DbSet<ProviderForm> ProviderForms { get; set; }
        public DbSet<CustomerReqForm> customerReqs { get; set; }
        public DbSet<ProviderLocation>  providerLocations { get; set; }
    }
}

