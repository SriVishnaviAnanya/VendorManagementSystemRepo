using Dev_2.Data;
using Dev_2.Models;
using Dev_2.DTOs;
using Dev_2.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dev_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendorsController(AppDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Vendor Onboarding
        [HttpPost]
        public async Task<IActionResult> CreateVendor(VendorCreateDto dto)
        {
            var vendor = new Vendor
            {
                VendorName = dto.VendorName,
                Category = dto.Category,
                PrimaryContact = dto.PrimaryContact,
                Status = VendorStatus.Draft
            };

            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return Ok(vendor);
        }

        // 2️⃣ Get All Vendors
        [HttpGet]
        public async Task<IActionResult> GetAllVendors()
        {
            return Ok(await _context.Vendors.ToListAsync());
        }

        // 3️⃣ Vendor Categorization & Search
        [HttpGet("search")]
        public async Task<IActionResult> SearchVendors(
            string? category,
            VendorStatus? status,
            RiskLevel? riskLevel)
        {
            var query = _context.Vendors.AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(v => v.Category == category);

            if (status.HasValue)
                query = query.Where(v => v.Status == status);

            if (riskLevel.HasValue)
                query = query.Where(v => v.RiskLevel == riskLevel);

            return Ok(await query.ToListAsync());
        }

        // 4️⃣ Vendor Status Control
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateVendorStatus(
            int id,
            VendorStatusUpdateDto dto)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
                return NotFound("Vendor not found");

            vendor.Status = dto.Status;
            await _context.SaveChangesAsync();

            return Ok(vendor);
        }

        // 5️⃣ Eligible Vendors for Contracts
        [HttpGet("eligible")]
        public async Task<IActionResult> GetEligibleVendors()
        {
            var vendors = await _context.Vendors
                .Where(v => v.Status == VendorStatus.Active)
                .ToListAsync();

            return Ok(vendors);
        }
    }
}