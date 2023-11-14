using CSTest.Db.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CSTest.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly ILogger<AuditController> _logger;

        public AuditController(ILogger<AuditController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Audit>> Get(DateTime? fromDate, DateTime? toDate)
        {
            List<Audit> auditList = new List<Audit>();
            using (var context = new CstestdbContext())
            {
                if (fromDate != null && toDate != null) 
                {                
                    auditList = context.Audits.Where(i => i.Date >= fromDate && i.Date <= toDate).OrderByDescending(i => i.Date).Take(10).ToList();
                }
                else
                {
                    auditList = context.Audits.OrderByDescending(i => i.Date).Take(10).ToList();
                }
            }
            return auditList;
        }
    }
}