using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
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
            modelBuilder.Entity<TripDetails>().OwnsOne(c => c.DestinationAddress);
            modelBuilder.Entity<TripDetails>().OwnsOne(c => c.StartingAddress);

            modelBuilder.Entity<TripUser>()
                .HasKey(tu => new { tu.TripId, tu.UserId });
            modelBuilder.Entity<TripUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TripList)
                .HasForeignKey(tu => tu.UserId);
            modelBuilder.Entity<TripUser>()
                 .HasOne(tu => tu.Trip)
                 .WithMany(u => u.Passangers)
                 .HasForeignKey(tu => tu.UserId);

            modelBuilder.Entity<ApplicationUser>();
        }
    }
}
