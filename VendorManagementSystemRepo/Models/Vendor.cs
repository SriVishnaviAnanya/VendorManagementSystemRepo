using Dev_2.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dev_2.Models
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string Category { get; set; }
        public string PrimaryContact { get; set; }

        public VendorStatus Status { get; set; } = VendorStatus.Draft;
        public RiskLevel RiskLevel { get; set; }
        public int ComplianceScore { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}