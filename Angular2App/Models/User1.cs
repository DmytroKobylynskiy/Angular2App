using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Angular2App.Models
{
    public class User1 
    {
        [Key]
        public int UserId { get; set; }

        public bool CarExist { get; set; }

        public string DriverLicense { get; set; }
    }
}
