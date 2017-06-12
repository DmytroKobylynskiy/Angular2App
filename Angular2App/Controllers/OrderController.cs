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
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular2App.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {

        private ApplicationDbContext db;
        private readonly ILogger _logger;
        public OrderController(ILoggerFactory loggerFactory,ApplicationDbContext context)
        {
            db = context;
            _logger = loggerFactory.CreateLogger<OrderController>();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> TaxiOrders()
        {
            
            List<TaxiOrder> taxiOrders = await db.TaxiOrders.ToListAsync();
            List<TaxiOrder> taxi = new List<TaxiOrder>();
            using(db){
                    var taxis = from n in db.TaxiOrders
                                        where n.OrderStatus=="Free"
                                        select n;
                    foreach (var notification in taxis)
                    {
                            taxi.Add(notification);
                    }
                    for (int i = 0; i < taxi.Count; i++)
                    {
                        if(taxi[i].StartPoint.IndexOf('|')>0){
                            taxi[i].StartPoint = taxi[i].StartPoint.Remove(0, taxi[i].StartPoint.IndexOf('|')+1);
                        }
                        if(taxi[i].EndPoint.IndexOf('|')>0){
                            taxi[i].EndPoint = taxi[i].EndPoint.Remove(0, taxi[i].EndPoint.IndexOf('|')+1);
                        }
                    }
                }
            return Json(taxi);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> TaxiOrdersCoords()
        {
            List<TaxiOrder> taxiOrders = await db.TaxiOrders.ToListAsync();
            List<TaxiOrder> taxi = taxiOrders;
            for (int i = 0; i < taxi.Count; i++)
            {
                if(taxiOrders[i].StartPoint.IndexOf('|')>0){
                     taxi[i].StartPoint = taxiOrders[i].StartPoint.Substring(0,taxiOrders[i].StartPoint.IndexOf('|'));
                }
            }
            return Json(taxi);
        }

        [HttpGet("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> MyOrders(string id)
        {
            List<TaxiOrder> taxi = new List<TaxiOrder>();
            using(db){
                    var taxis = from n in db.TaxiOrders
                                        where n.OrderOwnerId==id
                                        select n;
                    foreach (var notification in taxis)
                    {
                            taxi.Add(notification);
                    }
                    for (int i = 0; i < taxi.Count; i++)
                    {
                        if(taxi[i].StartPoint.IndexOf('|')>0){
                            taxi[i].StartPoint = taxi[i].StartPoint.Remove(0, taxi[i].StartPoint.IndexOf('|')+1);
                        }
                        if(taxi[i].EndPoint.IndexOf('|')>0){
                            taxi[i].EndPoint = taxi[i].EndPoint.Remove(0, taxi[i].EndPoint.IndexOf('|')+1);
                        }
                    }
            }
            return Json(taxi);
        }
/* 
        public async Task<IActionResult> MyRequests()
        {
            var user = await GetCurrentUserAsync();
            var userId = user.Id;
            List<TaxiOrder> allOrders = await db.TaxiOrders.ToListAsync();
            List<TaxiOrder> myRequest = new List<TaxiOrder>();
            foreach (var order in allOrders)
            {
                if (order.ReceiverId == userId)
                {
                    myRequest.Add(order);
                }
            }
            return View(myRequest);
        }
*/

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTaxiOrder([FromBody]TaxiOrder taxiOrder)
        {

           // var user = await GetCurrentUserAsync();
           // var userId = user?.Id;
           // taxiOrder.OrderOwnerId = userId
           List<TaxiOrder> allOrders = await db.TaxiOrders.ToListAsync();
           if(taxiOrder.ReceiverId==null){
               taxiOrder.OrderStatus = "Free";
               taxiOrder.Id=allOrders[allOrders.Count-1].Id+1;
                db.TaxiOrders.Add(taxiOrder);
               await db.SaveChangesAsync();
           }else{
               taxiOrder.OrderStatus = "In progress";
               taxiOrder.Id=allOrders[allOrders.Count-1].Id+1;
               db.TaxiOrders.Add(taxiOrder);
               await CreateNotification(taxiOrder);
               //await SendEmail(taxiOrder.Id,false);
               await db.SaveChangesAsync();
           }
           return Json(taxiOrder,new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
/*
        public async Task<IActionResult> CreateTaxiOrderToConcreateDriver(string receiverId)
        {
            TaxiOrder taxiOrder = new TaxiOrder();
           // taxiOrder.ReceiverId = receiverId;
            //db.TaxiOrders.Add(taxiOrder);
           // await db.SaveChangesAsync();
            return View(taxiOrder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaxiOrderToConcreateDriver(string receiverId,TaxiOrder taxiOrder)
        {

            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            //taxiOrder.OrderOwnerId = userId;
            taxiOrder.OrderStatus = "In Progress";
            db.TaxiOrders.Add(taxiOrder);
            await db.SaveChangesAsync();
            return RedirectToAction("TaxiOrders");
        }
*/
        [HttpGet("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> EditTaxiOrder(int? id)
        {
            if (id != null)
            {
                TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == id);
                taxiOrder.StartPoint = taxiOrder.StartPoint.Remove(0, taxiOrder.StartPoint.IndexOf('|')+1);
                taxiOrder.EndPoint = taxiOrder.EndPoint.Remove(0, taxiOrder.EndPoint.IndexOf('|')+1);
                _logger.LogInformation("OKOKOKOKO");
                //db.TaxiOrders.Remove(taxiOrder);
                    await db.SaveChangesAsync();
                if (taxiOrder != null)
                    return Json(taxiOrder,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
            return NotFound();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditTaxiOrder([FromBody]TaxiOrder taxiOrderNew)
        {
            //TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == taxiOrderNew.Id);
            //taxiOrder = taxiOrderNew;
            _logger.LogInformation("ID this "+taxiOrderNew.Id );
            db.TaxiOrders.Update(taxiOrderNew);
            _logger.LogInformation(taxiOrderNew.StartPoint);
            await db.SaveChangesAsync();
            return Json(taxiOrderNew.StartPoint,new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        

        [HttpGet("[action]")]
        [HttpGet("{index}")]
        public async Task<IActionResult> RemoveTaxiOrder(int index)
        {   
            List<TaxiOrder> taxi = await db.TaxiOrders.ToListAsync();
            if (index != null)
            {
                TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == index);
                if (taxiOrder != null)
                {
                    db.TaxiOrders.Remove(taxiOrder);
                    await db.SaveChangesAsync();
                    taxi = await db.TaxiOrders.ToListAsync();
                    return Json(taxi,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                }
                
            }
            return Json(taxi,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
        }

        [HttpGet("[action]")]
        [HttpGet("{id}")]
        [HttpGet("{receiverId}")]
        [HttpGet("{receiverEmail}")]
        public async Task<IActionResult> AgreeTaxiOrder(int? id,string receiverId, string receiverEmail)
        {
            if (id != null)
            {
                TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == id);
                if (taxiOrder != null)
                {
                    taxiOrder.OrderStatus = "In progress";
                    taxiOrder.ReceiverId = receiverId;
                    taxiOrder.ReceiverEmail = receiverEmail;
                    db.TaxiOrders.Update(taxiOrder);
                    await CreateNotification(taxiOrder);
                    //await SendEmail(taxiOrder.Id,true);
                    await db.SaveChangesAsync();
                    return Json(taxiOrder,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                }
            }
            return NotFound();
        }
        
        [HttpGet("[action]")]
        [HttpGet("{id}")]
        [HttpGet("{isOwner}")]
         public async Task<IActionResult> SendEmail(int id,string isOwner){
            NotificationOrder notificationOrder = await db.NotificationsOrder.FirstOrDefaultAsync(p => p.OrderId == id);
            EmailService emailService = new EmailService();
            if(isOwner=="order"){
                await emailService.SendEmailAsync(notificationOrder.OrderOwnerEmail, notificationOrder.NotificationTitle , notificationOrder.NotificationTitle );
            }else{
                await emailService.SendEmailAsync(notificationOrder.ReceiverEmail, notificationOrder.NotificationTitle , notificationOrder.NotificationTitle );
            }
            return Ok();
        }

        public async Task CreateNotification(TaxiOrder taxiOrder){
            NotificationOrder notificationOrder = new NotificationOrder();
                    notificationOrder.OrderOwnerId = taxiOrder.OrderOwnerId;
                    notificationOrder.OrderOwnerEmail = taxiOrder.OrderOwnerEmail;
                    notificationOrder.ReceiverId = taxiOrder.ReceiverId;
                    notificationOrder.ReceiverEmail = taxiOrder.ReceiverEmail;
                    notificationOrder.OrderId = taxiOrder.Id;
                    notificationOrder.OrderStatus = taxiOrder.OrderStatus;
                    notificationOrder.NotificationStatusClient = "Free";
                    notificationOrder.NotificationStatusCarrier = "Free";
                    notificationOrder.NotificationTitle = "Заказ " +  taxiOrder.StartPoint.Remove(0, taxiOrder.StartPoint.IndexOf('|')+1) + " - "
                        +taxiOrder.EndPoint.Remove(0, taxiOrder.EndPoint.IndexOf('|')+1)+" был принят. " ;
                    db.NotificationsOrder.Add(notificationOrder);
                    await db.SaveChangesAsync();
        }
        [HttpGet("[action]")]
        [HttpGet("{ownerId}")]
        public async Task<IActionResult> GetNotificationsOrder(string ownerId)
        {   
            
            if (ownerId != null)
            {
                List<NotificationOrder> ns = new List<NotificationOrder>() ; 
                using(db){
                    var notifications = from n in db.NotificationsOrder
                                        where n.OrderOwnerId == ownerId
                                        select n;
                    foreach (var notification in notifications)
                    {
                            ns.Add(notification);
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
        public async Task<IActionResult> GetNumNotificationsOrder(string ownerId)
        {   
            
            if (ownerId != null)
            {
                List<NotificationOrder> ns = new List<NotificationOrder>() ; 
                using(db){
                    var notifications = from n in db.NotificationsOrder
                                        where n.OrderOwnerId == ownerId
                                        select n;
                    return Json(notifications.Count(),new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                }
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> OrderById(int id)
        {   
            
            if (id != null)
            {
                TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == id);
                if (taxiOrder != null)
                {
                    return Json(taxiOrder,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                }
                
            }
            return NotFound();
        }


        [HttpGet("[action]")]
        [HttpGet("{id}")]
        [HttpGet("{duration}")]
        [HttpGet("{distanse}")]
        [HttpGet("{price}")]
        public async Task<IActionResult> ConfirmTaxiOrder(int id, float duration, float distanse,float price)
        {
            List<TaxiOrder> taxi = await db.TaxiOrders.ToListAsync();
            if (id != null)
            {
                _logger.LogInformation("adssad"+id);
                TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == id);
                
                if (taxiOrder != null)
                {
                    
                    taxiOrder.Distanse = distanse;
                    taxiOrder.Duration = duration;
                    taxiOrder.OrderStatus = "Done";
                    db.TaxiOrders.Update(taxiOrder);
                    await db.SaveChangesAsync();
                    price = price*distanse+duration*10;
                    return Json(price,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                }
            }
            return Json(taxi,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
        }
/*
        public async Task<IActionResult> ConfirmTaxiOrder(int? id)
        {
            TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == id);
            return View(taxiOrder);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDeleteTaxiOrder(int? id)
        {
            if (id != null)
            {
                TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == id);
                if (taxiOrder != null)
                    return View(taxiOrder);
            }
            return NotFound();
        }*/
    }
}
