using ERPSYS.Controllers.FinanceControll;
using ERPSYS.Models;
using Microsoft.EntityFrameworkCore;

namespace ERPSYS.Data // Ensure this matches your project's namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
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

