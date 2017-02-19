using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Angular2App.Data;
using Angular2App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Angular2App.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private ApplicationDbContext db;

        public UsersController(ApplicationDbContext context){
            db = context;
            
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Clients(){
            return Ok(db.Clients);
        }
    }
}
