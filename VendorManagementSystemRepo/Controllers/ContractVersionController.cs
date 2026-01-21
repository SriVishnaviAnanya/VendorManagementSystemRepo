using VendorManagementSystemRepo.Data;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

[ApiController]

[Route("api/contracts")]

public class ContractVersionController : ControllerBase

{

    private readonly VendorManagementSystemDb _context;

    public ContractVersionController(VendorManagementSystemDb context)

    {

        _context = context;

    }

    [HttpGet("{id}/active-version")]

    public async Task<IActionResult> GetActiveVersion(int id)

    {

        var version = await _context.ContractVersions

            .Where(v => v.ContractId == id)

            .OrderByDescending(v => v.VersionNumber)

            .FirstOrDefaultAsync();

        if (version == null)

            return NotFound("No approved versions found");

        return Ok(version);

    }

}
