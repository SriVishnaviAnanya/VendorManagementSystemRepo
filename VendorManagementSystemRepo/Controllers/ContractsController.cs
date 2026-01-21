using Dev3_Contract.Data;

using Dev3_Contract.DTOs;

using Dev3_Contract.Models;

using Microsoft.AspNetCore.Mvc;

namespace Dev3_Contract.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class ContractsController : ControllerBase

    {

        private readonly AppDbContext _context;

        public ContractsController(AppDbContext context)

        {

            _context = context;

        }

        [HttpPost]

        public async Task<IActionResult> CreateContract([FromBody] CreateContractDto dto)

        {

            // 1️⃣ Validate request

            if (!ModelState.IsValid)

            {

                return BadRequest(ModelState);

            }

            // 2️⃣ Business validation

            if (dto.EndDate <= dto.StartDate)

            {

                return BadRequest("EndDate must be greater than StartDate");

            }

            try

            {

                var contract = new Contract

                {

                    ContractNumber = dto.ContractNumber,

                    Title = dto.Title,

                    Description = dto.Description,

                    StartDate = dto.StartDate,

                    EndDate = dto.EndDate,

                    Status = ContractStatus.Draft,

                    CreatedAt = DateTime.UtcNow

                };

                _context.Contracts.Add(contract);

                await _context.SaveChangesAsync();

                return Ok(contract);

            }

            catch (Exception ex)

            {

                // 3️⃣ TEMP: expose real error (DEV only)

                return StatusCode(500, ex.Message);

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _context.Contracts.ToListAsync();
            return Ok(contracts);
        }

        [HttpGet("{contractId}")]

        public async Task<IActionResult> GetContractById(int contractId)

        {

            var contract = await _context.Contracts.FindAsync(contractId);

            if (contract == null)

            {

                return NotFound($"Contract with ContractId {contractId} not found");

            }

            return Ok(contract);

        }

        [HttpPatch("{contractId}/status")]

        public async Task<IActionResult> UpdateContractStatus(

            int contractId,

            [FromBody] UpdateContractStatusDto dto)

        {

            var contract = await _context.Contracts.FindAsync(contractId);

            if (contract == null)

            {

                return NotFound($"Contract with ContractId {contractId} not found");

            }

            contract.Status = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(contract);

        }

        [HttpPost("seed-vendor")]

        public async Task<IActionResult> SeedVendor()

        {

            var vendor = new Vendor

            {

                VendorName = "Wipro",

                Category = "IT Services",

                Email = "contact@wipro.com",

                Phone = "7777777777"

            };

            _context.Set<Vendor>().Add(vendor);

            await _context.SaveChangesAsync();

            return Ok(vendor);

        }

    }

}
