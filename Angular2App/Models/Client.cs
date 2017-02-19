using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Angular2App.Models
{
    public class Client 
    {
        [Key]
        public int ClientId { get; set; }

        public bool CarExist { get; set; }

        public string DriverLicense { get; set; }
    }
}
