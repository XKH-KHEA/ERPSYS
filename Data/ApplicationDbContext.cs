using Microsoft.EntityFrameworkCore;
using ERPSYS.Models;

namespace ERPSYS.Data // Ensure this matches your project's namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasColumnType("varchar(50)"); // Explicitly specify varchar for Username

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnType("varchar(200)"); // Explicitly specify varchar for PasswordHash
        }

    }

}

