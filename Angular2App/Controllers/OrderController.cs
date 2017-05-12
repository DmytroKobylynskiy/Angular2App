using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Angular2App.Data;
using Angular2App.Models;
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
            List<TaxiOrder> taxi = taxiOrders;
            for (int i = 0; i < taxi.Count; i++)
            {
                if(taxiOrders[i].StartPoint.IndexOf('|')>0){
                     taxi[i].StartPoint = taxiOrders[i].StartPoint.Remove(0, taxiOrders[i].StartPoint.IndexOf('|')+1);
                }
                if(taxiOrders[i].EndPoint.IndexOf('|')>0){
                     taxi[i].EndPoint = taxiOrders[i].EndPoint.Remove(0, taxiOrders[i].EndPoint.IndexOf('|')+1);
                }
            }
            _logger.LogInformation("OIKOKOEDQSSDSDASDSD");
            /*
            SortState.SortingState sortOrder = SortState.SortingState.StartPointAsc;
            ViewData["StartPointSort"] = sortOrder == SortState.SortingState.StartPointAsc ? SortState.SortingState.StartPointDesc : SortState.SortingState.StartPointAsc;
            ViewData["EndPointSort"] = sortOrder == SortState.SortingState.EndPointAsc ? SortState.SortingState.EndPointDesc : SortState.SortingState.EndPointAsc;
            ViewData["DateSort"] = sortOrder == SortState.SortingState.DateAsc ? SortState.SortingState.DateDesc : SortState.SortingState.DateAsc;
            ViewData["StatusSort"] = sortOrder == SortState.SortingState.StatusOrderAsc ? SortState.SortingState.StatusOrderDesc : SortState.SortingState.StatusOrderAsc;

            switch (sortOrder)
            {
                case SortState.SortingState.StartPointDesc:
                    taxi = taxi.OrderByDescending(s => s.StartPoint).ToList();
                    break;
                case SortState.SortingState.EndPointAsc:
                    taxi = taxi.OrderBy(s => s.EndPoint).ToList();
                    break;
                case SortState.SortingState.EndPointDesc:
                    taxi = taxi.OrderByDescending(s => s.EndPoint).ToList();
                    break;
                case SortState.SortingState.DateAsc:
                    taxi = taxi.OrderBy(s => s.Date).ToList();
                    break;
                case SortState.SortingState.DateDesc:
                    taxi = taxi.OrderByDescending(s => s.Date).ToList();
                    break;
                case SortState.SortingState.StatusOrderAsc:
                    taxi = taxi.OrderBy(s => s.OrderStatus).ToList();
                    break;
                case SortState.SortingState.StatusOrderDesc:
                    taxi = taxi.OrderByDescending(s => s.OrderStatus).ToList();
                    break;
                default:
                    taxi = taxi.OrderBy(s => s.StartPoint).ToList();
                    break;
            }*/
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
/*
        public async Task<IActionResult> MyOrders()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            List<TaxiOrder> allOrders = await db.TaxiOrders.ToListAsync();
            List<TaxiOrder> myOrders = new List<TaxiOrder>();
            foreach (var order in allOrders)
            {
                if (order.OrderOwnerId == userId)
                {
                    myOrders.Add(order);
                }
            }
            return View(myOrders);
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
           // taxiOrder.OrderOwnerId = userId;
           _logger.LogInformation("ads"+taxiOrder.ReceiverId+"adas");
           if(taxiOrder.ReceiverId==null){
               taxiOrder.OrderStatus = "Free";
           }else{
               taxiOrder.OrderStatus = "In progress";
           }
            db.TaxiOrders.Add(taxiOrder);
            await db.SaveChangesAsync();
            
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
            _logger.LogInformation("OKOKOKOKO"+id);
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
            _logger.LogInformation("OIKOKOEDQSSDSDASDSD"+index);
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
        public async Task<IActionResult> AgreeTaxiOrder(int? id,string receiverId)
        {
            _logger.LogInformation("O=OKO"+id);
            if (id != null)
            {
                TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == id);
                if (taxiOrder != null)
                {
                    taxiOrder.OrderStatus = "In progress";
                    taxiOrder.ReceiverId = receiverId;
                    db.TaxiOrders.Update(taxiOrder);
                    NotificationOrder notificationOrder = new NotificationOrder();
                    notificationOrder.OrderOwnerId = taxiOrder.OrderOwnerId;
                    notificationOrder.ReceiverId = receiverId;
                    notificationOrder.OrderId = taxiOrder.Id;
                    notificationOrder.OrderStatus = taxiOrder.OrderStatus;
                    notificationOrder.NotificationStatus = "Free";
                    notificationOrder.NotificationTitle = "Заказ " + id + "был принят. " ;
                    db.NotificationsOrder.Add(notificationOrder);
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
            if (id != null)
            {
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
            return NotFound();
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
