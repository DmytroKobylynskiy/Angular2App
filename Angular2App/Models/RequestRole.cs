using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
namespace Angular2App.Models
{
    public class RequestRole
    {
        [Key]
        public int RequestId { get; set; }
        public string RequestOwnerId { get; set; }
        public string NewRole { get; set; }
        public string RequestStatus { get; set; }
        public string DriverLicense { get; set; }
    }
}
