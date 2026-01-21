using System.ComponentModel.DataAnnotations;

namespace GovernanceApi.Models
{
    public class VendorPerformance
    {
        [Key]
        public int PerformanceId { get; set; }  
        public int VendorId { get; set; }   
        public int DeliveryQuality  { get; set; }   
        public int SLAAdherence { get; set; }
        public string SLARemarks { get; set; }  
        public int ComplianceScore { get; set; }
        public int IssueCount { get; set; }
        //public int FinalRating { get; set; }
        public int Penalty { get; set; }
        public decimal  FinalScore { get; set; }
        public string VendorRating { get; set; }
        public DateTime CalculatedDate { get; set; } = DateTime.Now;
        public DateTime SLARatedDate { get; set; }
    }
}
