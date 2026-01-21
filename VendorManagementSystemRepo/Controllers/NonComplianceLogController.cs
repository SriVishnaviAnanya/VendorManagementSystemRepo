using VendorManagementSystemRepo.Data;
using VendorManagementSystemRepo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VendorManagementSystemRepo.Controllers
{
    [EnableCors("Open")]
    [Route("api/[controller]")]
    [ApiController]
    public class NonComplianceLogController : ControllerBase
    {
        private readonly VendorManagementSystemDb _dbContext;

        public NonComplianceLogController(VendorManagementSystemDb context)
        {
            _dbContext = context;
        }

        // GET: api/NonComplianceLog
        // This lets you see the data (currently empty in your SQL screenshot)
        [HttpGet]
        public IActionResult GetLogs()
        {
            var logs = _dbContext.NonComplianceLogs.ToList();
            return Ok(logs);
        }

        // POST: api/NonComplianceLog
        // This lets you add new logs via Swagger
        [HttpPost]
        public IActionResult AddNonComplianceLog([FromBody] NonComplianceLog log)
        {
            if(log.CreatedDate == default)
            {
                log.CreatedDate = DateTime.Now;
            }
            if (log.Severity?.Trim().ToLower()=="major")
            {
                log.Penalty = 20;
            }
            else
            {
                log.Penalty = 5;
            }
            _dbContext.NonComplianceLogs.Add(log);
            _dbContext.SaveChanges();
            return Ok(log);
        }
    }
}

