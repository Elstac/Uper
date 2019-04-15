using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Entities;

namespace WebApp.Data
{
    public class ApplicationContext:IdentityDbContext<ApplicationUser>
    {
        //public  DbSet<ApplicationUser> Users { get; set; }
        public DbSet<TripDetails> TripDetails { get; set; }
        public DbSet<TripUser> TripUser { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            modelBuilder.Entity<ApplicationUser>();
        }
    }
}
