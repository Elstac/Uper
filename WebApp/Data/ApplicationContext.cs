using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Data.Entities;

namespace WebApp.Data
{
    public class ApplicationContext:IdentityDbContext<ApplicationUser>
    {
        //public  DbSet<ApplicationUser> Users { get; set; }
        public DbSet<TripDetails> TripDetails { get; set; }
        public DbSet<TripUser> TripUser { get; set; }
        public DbSet<RatesAndComment> RatesAndComment { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API
            modelBuilder.Entity<TripDetails>().OwnsOne(c => c.DestinationAddress);
            modelBuilder.Entity<TripDetails>().OwnsOne(c => c.StartingAddress);

            BuildTripUserEntity(modelBuilder.Entity<TripUser>());

            modelBuilder.Entity<ApplicationUser>();
            modelBuilder.Entity<RatesAndComment>();

        }

        private void BuildTripUserEntity(EntityTypeBuilder<TripUser> builder)
        {
            builder.HasKey(tu => new { tu.TripId, tu.UserId });

            builder.HasOne(tu => tu.User)
                .WithMany(u => u.TripList)
                .HasForeignKey(tu => tu.UserId);

            builder.HasOne(tu => tu.Trip)
                 .WithMany(u => u.Passangers)
                 .HasForeignKey(tu => tu.TripId);

            builder.Property(td => td.Accepted)
                .HasDefaultValue(false);

        }
    }
}
