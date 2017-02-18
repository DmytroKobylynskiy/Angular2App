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
        private DbContext db;

        public UsersController(DbContext _db){
            db = _db;
            User1 user = new User1();
            user.CarExist=true;
            user.DriverLicense ="133";
            db.Add(user);
            db.SaveChangesAsync();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Clients(){

            return Ok(new
                    {
                        Temp = db.Users.ToString()
                    });
        }
    }
}
