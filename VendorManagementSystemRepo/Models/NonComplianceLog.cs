using System.ComponentModel.DataAnnotations;

namespace VendorManagementSystemRepo.Models
{
    public class NonComplianceLog
    {
        [Key]
        public int NonComplianceId { get; set; }
        public int VendorId { get; set; }
        public int ContractId { get; set; }
        public string Reason { get; set; }
        public bool Escalated { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Severity { get; set; }
        public int Penalty { get; set; }

    }
}
