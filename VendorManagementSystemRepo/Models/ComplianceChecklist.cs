using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendorManagementSystemRepo.Models
{
    public class ComplianceChecklist
    {
        [Key]
        public int ComplianceId { get; set; }
        public int VendorId { get; set; }
        public bool NDASigned { get; set; }
        public bool CertificationsValid { get; set; }
        public bool RegulatoryCompliant { get; set; }
        public int ComplianceScore { get; set; }
        public string ComplianceStatus { get; set; }
        public DateTime LastReviewDate { get; set; }
    }
}