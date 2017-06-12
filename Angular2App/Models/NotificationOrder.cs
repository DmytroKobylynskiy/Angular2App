using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
namespace Angular2App.Models
{
    public class NotificationOrder 
    {
        [Key]
        public int NotificationId { get; set; }

        public string NotificationTitle{ get; set;}

        public string NotificationStatusClient{get;set;}

        public string NotificationStatusCarrier{get;set;}

        public string ReceiverId { get; set; }

        public string ReceiverEmail {get;set;}

        public string OrderOwnerId { get; set; }

        public string OrderOwnerEmail{get;set;}

        public string OrderStatus {get;set;}

        public int OrderId {get;set;}

        public bool Condition{get;set;}
        
    }
}
