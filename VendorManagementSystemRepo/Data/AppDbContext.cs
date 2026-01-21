using VendorManagementSystemRepo.Models;
using Microsoft.EntityFrameworkCore;
using VendorManagementSystemRepo.Models;
namespace VendorManagementSystemRepo.Data
{
    public class VendorManagementSystemDb : DbContext
    {
        public VendorManagementSystemDb(DbContextOptions<VendorManagementSystemDb> options)
            : base(options)
        {}
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<ComplianceChecklist> ComplianceChecklists { get; set; }
        public DbSet<NonComplianceLog> NonComplianceLogs { get; set; }
        public DbSet<VendorPerformance> VendorPerformances { get; set; }
        public object Vendors { get; internal set; }
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractVersion> ContractVersions { get; set; }
        //public ApplicationDbContext CreateDbContext(string[] args)
        //{
        //    IConfiguration configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        //    optionsBuilder.UseSqlServer(
        //        configuration.GetConnectionString("DefaultConnection")
        //    );
        //    return new ApplicationDbContext(optionsBuilder.Options);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComplianceChecklist>(entity =>
            {
                entity.HasKey(e => e.ComplianceId);
                entity.Property(e => e.ComplianceStatus)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(e => e.LastReviewDate)
                    .HasDefaultValueSql("GETDATE()");
                entity.HasIndex(e => e.VendorId);
            });

            modelBuilder.Entity<NonComplianceLog>(entity =>
            {
                entity.HasKey(e => e.NonComplianceId);
                entity.Property(e => e.Reason)
                    .HasMaxLength(1000)
                    .IsRequired();
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");
                entity.HasIndex(e => e.VendorId);
                entity.HasIndex(e => e.ContractId);
            });

            modelBuilder.Entity<VendorPerformance>(entity =>
            {
                entity.HasKey(e => e.PerformanceId);
                entity.Property(e => e.SLARemarks)
                    .HasMaxLength(1000);
                entity.Property(e => e.SLARatedDate)
                    .HasDefaultValueSql("GETDATE()");
                entity.HasIndex(e => e.VendorId);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}