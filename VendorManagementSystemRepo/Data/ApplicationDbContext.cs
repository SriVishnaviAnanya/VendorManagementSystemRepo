using Microsoft.EntityFrameworkCore;
using Dev3_Contract.Models;
namespace Dev3_Contract.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractVersion> ContractVersions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>()
                .Property(c => c.ContractValue)
                .HasPrecision(18, 2);
            modelBuilder.Entity<ContractVersion>()
                .Property(cv => cv.ContractValue)
                .HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }
    }
}