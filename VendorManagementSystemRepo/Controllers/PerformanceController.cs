using GovernanceApi.Data;
using GovernanceApi.DTOs;
using GovernanceApi.Models;
using GovernanceApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GovernanceApi.Controllers
{
    [EnableCors("Open")]
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PerformanceService _performanceService;
        public PerformanceController(AppDbContext context, PerformanceService performanceService)
        {
            _context = context;
            _performanceService = performanceService;
        }
        
        [HttpPost("add-performance")]
        public IActionResult AddPerformance([FromBody] VendorPerformanceInputDto dto)
        {
            var compliance = _context.ComplianceChecklists
                .FirstOrDefault(c => c.VendorId == dto.VendorId);

            if (compliance == null)
                return BadRequest("Compliance checklist not found for the vendor");

            var issues = _context.NonComplianceLogs
                .Where(i => i.VendorId == dto.VendorId)
                .ToList();

            int issueCount = issues.Count;
            int penalty = _performanceService.CalculatePenalty(issues);

            var performance = new VendorPerformance
            {
                VendorId = dto.VendorId,
                DeliveryQuality = dto.DeliveryQuality,
                SLAAdherence = dto.SLAAdherence,
                SLARemarks = dto.SLARemarks,
                ComplianceScore = compliance.ComplianceScore,
                IssueCount = issueCount,
                Penalty = penalty
            };

            var finalResult =
                _performanceService.CalculateFinalPerformance(performance, penalty, 0); // Replace 0 with actual calculatedValue if needed

            _context.VendorPerformances.Add(finalResult);
            _context.SaveChanges();

            return Ok(finalResult);
        }
        [HttpPut("update-sla/{vendorId}")]
        public IActionResult UpdateSLA(int vendorId, [FromBody] SLAUpdateDto dto)
        {
            var p = _context.VendorPerformances.FirstOrDefault(x => x.VendorId == vendorId);
            if (p == null) return NotFound("Vendor performance not found");
            p.SLAAdherence = dto.SLAAdherence;
            p.SLARemarks = dto.SLARemarks;
            p.SLARatedDate = DateTime.Now;
            //CalculateFinalRating(p);
            _context.SaveChanges();
            return Ok(p);

        }
        //private void CalculateFinalRating(VendorPerformance p) {
        //    p.FinalRating = (p.DeliveryQuality + p.SLAAdherence + p.ComplianceScore) / 3;
        //}
    }
}
