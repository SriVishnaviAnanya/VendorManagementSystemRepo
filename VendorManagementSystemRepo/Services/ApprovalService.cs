public class ApprovalService

{

    private readonly VclmDbContext _context;

    public ApprovalService(VclmDbContext context)

    {

        _context = context;

    }

    public async Task SubmitForApproval(Guid contractId, Guid vendorId,

        string vendorName, string contractNumber)

    {

        var approvals = new List<ContractApproval>

        {

            new ContractApproval

            {

                ContractId = contractId,

                VendorId = vendorId,

                VendorName = vendorName,

                ContractNumber = contractNumber,

                ApproverRole = "Legal",

                Status = "Pending",

                ApprovalLevel = 1,

                ActionDate = DateTime.UtcNow,

                Comments = null

            },

            new ContractApproval

            {

                ContractId = contractId,

                VendorId = vendorId,

                VendorName = vendorName,

                ContractNumber = contractNumber,

                ApproverRole = "Finance",

                Status = "Pending",

                ApprovalLevel = 2,

                ActionDate = DateTime.UtcNow

            }

        };

        _context.ContractApprovals.AddRange(approvals);

        await _context.SaveChangesAsync();

    }

    public async Task Approve(int approvalId, string comments)
    {
        var approval = await _context.ContractApprovals.FindAsync(approvalId);
        if (approval == null)
            throw new Exception($"Approval record not found for ID {approvalId}");
        approval.Status = "Approved";
        approval.ActionDate = DateTime.UtcNow;
        approval.Comments = comments;
        _context.ApprovalHistories.Add(new ApprovalHistory
        {
            ContractId = approval.ContractId,
            ApproverRole = approval.ApproverRole,
            ActionTaken = "Approved",
            Comments = comments,
            Timestamp = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
    }

    public async Task Reject(int approvalId, string comments)
    {
        var approval = await _context.ContractApprovals.FindAsync(approvalId);
        if (approval == null)
            throw new Exception($"Approval record not found for ID {approvalId}");
        approval.Status = "Rejected";
        approval.ActionDate = DateTime.UtcNow;
        approval.Comments = comments;
        _context.ApprovalHistories.Add(new ApprovalHistory
        {
            ContractId = approval.ContractId,
            ApproverRole = approval.ApproverRole,
            ActionTaken = "Rejected",
            Comments = comments,
            Timestamp = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
    }
}