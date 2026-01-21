public class ContractApproval
{
    public int Id { get; set; }
    public Guid ContractId { get; set; }
    public Guid VendorId { get; set; }
    public string VendorName { get; set; }
    public string ContractNumber { get; set; }
    public string ApproverRole { get; set; }
    public string Status { get; set; }
    public string? Comments { get; set; }   // ✅ Nullable
    public DateTime ActionDate { get; set; }
    public int ApprovalLevel { get; set; }
}