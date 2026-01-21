using System.ComponentModel.DataAnnotations.Schema;

namespace VendorManagementSystemRepo.Models
{
    public class ContractVersion
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public int VersionNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ContractValue { get; set; }
    }
}