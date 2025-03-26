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
            // public DbSet<Review> Reviews { get; set; }
            // public DbSet<Connection> Connections { get; set; }
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
                }
        }
}
