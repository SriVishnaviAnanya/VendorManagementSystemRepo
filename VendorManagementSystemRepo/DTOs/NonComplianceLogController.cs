using GovernanceApi.Data;
using GovernanceApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GovernanceApi.Controllers
{
    [EnableCors("Open")]
    [Route("api/[controller]")]
    [ApiController]
    public class NonComplianceLogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NonComplianceLogController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/NonComplianceLog
        // This lets you see the data (currently empty in your SQL screenshot)
        [HttpGet]
        public IActionResult GetLogs()
        {
            var logs = _context.NonComplianceLogs.ToList();
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
            _context.NonComplianceLogs.Add(log);
            _context.SaveChanges();
            return Ok(log);
        }
    }
}

