using Microsoft.EntityFrameworkCore;
using ERPSYS.Models;

namespace ERPSYS.Data // Ensure this matches your project's namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}

