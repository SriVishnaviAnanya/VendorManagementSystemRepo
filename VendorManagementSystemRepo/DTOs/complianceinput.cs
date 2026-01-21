namespace VendorManagementSystemRepo.DTOs
{
    public class complianceinput
    {
        public int VendorId { get; set; }
        public bool NDASigned { get; set; }
        public bool CertificationsValid { get; set; }
        public bool ReguatoryCompliant { get; set; }
    }
}
