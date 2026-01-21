using GovernanceApi.Data;
using GovernanceApi.DTOs;
using GovernanceApi.Models;
using GovernanceApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GovernanceApi.Controllers
{
    [EnableCors("Open")]
    [Route("api/[controller]")]
    [ApiController]

    public class ComplianceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IComplianceService _service;
        public ComplianceController(AppDbContext context, IComplianceService service)
        {
            _context = context;
            _service = service;
          
        }
        [HttpGet("{vendorId}")]
        public async Task<IActionResult>GetCompliance(int vendorId)
        {
            var complianceRecord = await _context.ComplianceChecklists
                .FirstOrDefaultAsync(c=> c.VendorId == vendorId);
            if(complianceRecord==null)
            {
                return NotFound($"No compliance record found for this VendorId {vendorId}");
            }
            return Ok(complianceRecord);
        }

        [HttpPost]
        public IActionResult EvaluateCompliance([FromBody] complianceinput dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = new ComplianceChecklist
            {
                VendorId = dto.VendorId,
                NDASigned = dto.NDASigned,
                CertificationsValid = dto.CertificationsValid,
                RegulatoryCompliant = dto.ReguatoryCompliant
            };

            var result = _service.EvaluateCompliance(model);
            model.ComplianceScore = result.ComplianceScore;
            model.ComplianceStatus = result.ComplianceStatus;
            model.ComplianceId = 0;
            _context.ComplianceChecklists.Add(model);
            _context.SaveChanges();
            return Ok(result);
              //if (model == null)
            //        return BadRequest("Invalid compliance checklist data.");
            //    bool vendorExists = _context.Vendors.Any(v => v.VendorId == model.VendorId);
            //    if (!vendorExists)
            //        return NotFound($"Vendor with ID {model.VendorId} does not exist.");
            //    var evaluated = _service.EvaluateCompliance(model);
            //    evaluated.ComplianceId = 0;
            //    try
            //    {
            //        _context.ComplianceChecklists.Add(evaluated);
            //        _context.SaveChanges();
            //    }
            //    catch (DbUpdateException ex)
            //    {
            //        return BadRequest(ex.InnerException?.Message ?? ex.Message);
            //    }
            //    return Ok(evaluated);
            //}

            ////_service.EvaluateCompliance(model);
            ////_context.ComplianceChecklists.Add(model);
            ////return Ok(model);
        }

    }
}
        //private string GetComplianceStatus(ComplianceChecklist c)
        //{
        //    if (c.NDASigned && c.CertificationsValid && c.RegulatoryCompliant)
        //        return "Compliant";
        //    if (c.NDASigned || c.CertificationsValid || c.RegulatoryCompliant) return "Partially Compliant";
        //    return "Non-Compliant";
        //}
        //private readonly AppDbContext _context;
        //public ComplianceController(AppDbContext context)
        //{
        //    _context = context;
        //}
        //[HttpGet]
        //public IActionResult AddCompliance(ComplianceChecklist model)
        //{
        //    model.ComplianceStatus = GetComplianceStatus(model);
        //    model.LastReviewDate = DateTime.Now;
        //    _context.ComplianceChecklists.Add(model);
        //    _context.SaveChanges();
        //    return Ok(model);


//}
//[HttpPost("non-compliance")]
//public IActionResult LogNonCompliance(NonComplianceLog log)
//{
//    log.CreatedDate = DateTime.Now;
//    _context.NonComplianceLogs.Add(log);
//    _context.SaveChanges();
//    return Ok(log);



