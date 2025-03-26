using Microsoft.EntityFrameworkCore;
using TheFundersJunction.Models;

namespace TheFundersJunction.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        // DbSets for Users and related entities
        public DbSet<User> Users { get; set; }
        public DbSet<SavedProfile> SavedProfiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Connection> Connections { get; set; }
        // public DbSet<Message> Messages { get; set; }
        // public DbSet<ProfileView> ProfileViews { get; set; }

        // Configure the relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SavedProfile>()
                .HasOne(sp => sp.User)
                .WithMany(u => u.SavedProfiles)
                .HasForeignKey(sp => sp.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany()
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ReviewedUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ReviewedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the Connection model relationships
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.User)
                .WithMany(u => u.Connections)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.ConnectedUser)
                .WithMany()  // No reverse navigation property for simplicity
                .HasForeignKey(c => c.ConnectedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}