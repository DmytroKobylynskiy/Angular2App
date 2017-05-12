using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Angular2App.Data;
using Angular2App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Angular2App.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly ILogger _logger;
        private ApplicationDbContext db;
        public RoleController(ILoggerFactory loggerFactory,ApplicationDbContext context)
        {
            db = context;
            _logger = loggerFactory.CreateLogger<RoleController>();
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ChangeRoleRequests()
        {
            List<RequestRole> requests = new List<RequestRole>();
            
            using(db){
                    var changeRoleRequests = from r in db.RequestsChangeRole
                                        where r.RequestStatus == "New"
                                        select r;
                    foreach (var request in changeRoleRequests)
                    {
                            requests.Add(request);
                    }
                    requests = await db.RequestsChangeRole.ToListAsync();
                    return Json(requests,new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                }
            return Json("ne ok");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateChangeRoleRequest([FromBody]RequestRole requestRole)
        {
            _logger.LogInformation("dasdsadasdasdas");
            if (requestRole!=null)
            {
                requestRole.RequestStatus = "New";
                db.RequestsChangeRole.Add(requestRole);
                await db.SaveChangesAsync();
                _logger.LogInformation("dasdsadasdasdas");
                return Json("OK");
            }
            _logger.LogInformation("dasdsadasdasdas");
            return Json("ne ok");
        }

        [HttpGet("[action]")]
        [HttpGet("{id}")]
        [HttpGet("{status}")]
        public async Task<IActionResult> SetRequestStatus(int id,string status)
        {   
            if (id != null)
            {
                RequestRole request = await db.RequestsChangeRole.FirstOrDefaultAsync(p => p.RequestId == id);
                if (request != null)
                {
                    request.RequestStatus = status;
                    db.RequestsChangeRole.Update(request);
                    await db.SaveChangesAsync();
                    return Json(request,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                } 
            }
            return NotFound();
        }
    }
}