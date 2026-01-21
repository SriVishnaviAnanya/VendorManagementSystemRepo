using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Dev3_Contract.Models
{
    public class Contract
    {
        public int ContractId { get; set; }
        [Required]
        public string ContractNumber { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        // ✅ FIX: Proper decimal mapping
        [Column(TypeName = "decimal(18,2)")]
        public decimal ContractValue { get; set; }
        // ✅ FIX: Default enum value
        public ContractStatus Status { get; set; } = ContractStatus.Draft;
        public DateTime CreatedAt { get; internal set; }
    }
}
