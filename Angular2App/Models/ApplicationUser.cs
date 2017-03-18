using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Angular2App.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int ApplicationUserId { get; set; }
        public bool CarExist { get; set; }
        public string Year { get; set; }
        public string DriverLicense { get; set; }
    }
}
