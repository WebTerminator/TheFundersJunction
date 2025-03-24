using Microsoft.EntityFrameworkCore;
using TheFundersJunction.Models;

namespace TheFundersJunction.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
