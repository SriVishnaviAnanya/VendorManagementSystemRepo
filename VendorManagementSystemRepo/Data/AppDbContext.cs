using Dev3_Contract.Models;
using Microsoft.EntityFrameworkCore;
namespace Dev3_Contract.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractVersion> ContractVersions { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

    }
}