namespace VendorManagementSystemRepo.Models
{
    public class VendorDocument
    {
        public int VendorDocumentId { get; set; }
        public int VendorId { get; set; }
        public string DocumentType { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
