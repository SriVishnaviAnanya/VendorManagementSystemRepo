using Dev3_Contract.Data;
using Dev3_Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/contracts")]
public class ContractApprovalController : ControllerBase
{
    private readonly AppDbContext _context;
    public ContractApprovalController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost("{id}/submit")]
    public async Task<IActionResult> SubmitForReview(int id)
    {
        var contract = await _context.Contracts.FindAsync(id);
        if (contract == null) return NotFound();
        contract.Status = ContractStatus.UnderReview;
        await _context.SaveChangesAsync();
        return Ok("Contract sent for review");
    }
    [HttpPost("{id}/approve")]
    public async Task<IActionResult> ApproveContract(int id)
    {
        var contract = await _context.Contracts.FindAsync(id);
        if (contract == null) return NotFound();
        contract.Status = ContractStatus.Approved;
        // ✅ Create version on approval
        var versionCount = await _context.ContractVersions
            .CountAsync(v => v.ContractId == id);
        _context.ContractVersions.Add(new ContractVersion
        {
            ContractId = id,
            VersionNumber = versionCount + 1,
            Title = contract.Title,
            Description = contract.Description,
            CreatedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
        return Ok("Contract approved & version created");
    }
}