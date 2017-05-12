using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Angular2App.Models
{
    public class NotificationOrder 
    {
        [Key]
        public int NotificationId { get; set; }

        public string NotificationTitle{ get; set;}

        public string NotificationStatus{get;set;}

        public string ReceiverId { get; set; }

        public string OrderOwnerId { get; set; }

        public string OrderStatus {get;set;}

        public int OrderId {get;set;}

        public bool Condition{get;set;}
        
    }
}
