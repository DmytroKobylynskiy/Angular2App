using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Angular2App.Data;
using Angular2App.Models;
using Angular2App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace Angular2App.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    
    public class NotificationController : Controller
    {
        private ApplicationDbContext db;

        public NotificationController(ApplicationDbContext context)
        {
            db = context;
        }
        
        [HttpGet("[action]")]
        [HttpGet("{ownerId}")]
        [HttpGet("{role}")]
        public async Task<IActionResult> GetNotificationsOrder(string ownerId,string role)
        {   
            
            if (ownerId != null)
            {
                List<NotificationOrder> ns = new List<NotificationOrder>() ; 
                using(db){

                    if(role=="carrier"){
                        var notifications = from n in db.NotificationsOrder
                                        where (n.OrderOwnerId == ownerId || n.ReceiverId == ownerId)&&n.NotificationStatusCarrier=="Free"
                                        select n;
                        foreach (var notification in notifications)
                        {
                            ns.Add(notification);
                        }
                    }else if(role=="customer"){
                        var notifications = from n in db.NotificationsOrder
                                        where (n.OrderOwnerId == ownerId || n.ReceiverId == ownerId)&&n.NotificationStatusClient=="Free"
                                        select n;
                        foreach (var notification in notifications)
                        {
                            ns.Add(notification);
                        }
                    }
                    
                    return Json(ns,new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                }
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        [HttpGet("{ownerId}")]
        [HttpGet("{role}")]
        public async Task<IActionResult> GetNumNotificationsOrder(string ownerId,string role)
        {   
            
            if (ownerId != null)
            {
                List<NotificationOrder> ns = new List<NotificationOrder>() ; 
                using(db){
                    if(role=="carrier"){
                        var notifications = from n in db.NotificationsOrder
                                        where (n.OrderOwnerId == ownerId || n.ReceiverId == ownerId)&&n.NotificationStatusCarrier=="Free"
                                        select n;
                        foreach (var notification in notifications)
                        {
                            ns.Add(notification);
                        }
                    }else if(role=="customer"){
                        var notifications = from n in db.NotificationsOrder
                                        where (n.OrderOwnerId == ownerId || n.ReceiverId == ownerId)&&n.NotificationStatusClient=="Free"
                                        select n;
                        foreach (var notification in notifications)
                        {
                            ns.Add(notification);
                        }
                    }
                    return Json(ns.Count(),new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                }
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        [HttpGet("{id}")]
        [HttpGet("{Status}")]
        public async Task<IActionResult> SetNotificationStatusCarrier(int id,string status)
        {   
            
            if (id != null)
            {
                NotificationOrder notification = await db.NotificationsOrder.FirstOrDefaultAsync(n => n.NotificationId == id);
                notification.NotificationStatusCarrier = status;
                db.NotificationsOrder.Update(notification);
                await db.SaveChangesAsync();
                List<NotificationOrder> ns = await db.NotificationsOrder.ToListAsync();
                return Json(ns,new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        [HttpGet("{id}")]
        [HttpGet("{Status}")]
        public async Task<IActionResult> SetNotificationStatusClient(int id,string status)
        {   
            
            if (id != null)
            {
                NotificationOrder notification = await db.NotificationsOrder.FirstOrDefaultAsync(n => n.NotificationId == id);
                notification.NotificationStatusClient = status;
                db.NotificationsOrder.Update(notification);
                await db.SaveChangesAsync();
                List<NotificationOrder> ns = await db.NotificationsOrder.ToListAsync();
                return Json(ns,new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                
            }
            return NotFound();
        }
        
    }
}
