public class ApprovalHistory
{
    public int Id { get; set; }
    public Guid ContractId { get; set; }
    public string ApproverRole { get; set; }
    public string ActionTaken { get; set; } // Approved, Rejected
    public string Comments { get; set; }
    public DateTime Timestamp { get; set; }
}