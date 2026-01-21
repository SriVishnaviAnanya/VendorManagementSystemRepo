using Dev3_Contract.Data;
using Dev3_Contract.Models;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/contracts")]
public class ContractRenewalController : ControllerBase
{
    private readonly AppDbContext _context;
    public ContractRenewalController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost("{id}/renew")]
    public async Task<IActionResult> RenewContract(int id, DateTime newEndDate)
    {
        var contract = await _context.Contracts.FindAsync(id);
        if (contract == null) return NotFound();
        contract.EndDate = newEndDate;
        contract.Status = ContractStatus.Active;
        await _context.SaveChangesAsync();
        return Ok("Contract renewed");
    }
}