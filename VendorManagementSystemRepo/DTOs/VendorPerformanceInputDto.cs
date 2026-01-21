namespace VendorManagementSystemRepo.DTOs
{
    public class VendorPerformanceInputDto
    {
        public int VendorId { get; set; }
        public int DeliveryQuality { get; set; } // 1-10
        public int SLAAdherence { get; set; } // 1-10
        public string SLARemarks { get; set; }
    }
}
