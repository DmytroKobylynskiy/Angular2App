using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Angular2App.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Angular2App.Controllers
{
    public class UsersController : Controller
    {
        private DbContext db;

        public UsersController(DbContext _db){
            db = _db;
        }
    }
}
