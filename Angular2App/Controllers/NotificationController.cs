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
        public async Task<IActionResult> GetNotificationsOrder(string ownerId)
        {   
            
            if (ownerId != null)
            {
                List<NotificationOrder> ns = new List<NotificationOrder>() ; 
                using(db){
                    var notifications = from n in db.NotificationsOrder
                                        where n.OrderOwnerId == ownerId || n.ReceiverId == ownerId
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
        [HttpGet("{Status}")]
        public async Task<IActionResult> SetNotificationStatusRead(int id,string status)
        {   
            
            if (id != null)
            {
                NotificationOrder notification = await db.NotificationsOrder.FirstOrDefaultAsync(n => n.NotificationId == id);
                db.NotificationsOrder.Update(notification);
                await db.SaveChangesAsync();
                return Json(notification,new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                
            }
            return NotFound();
        }
/*
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOffer([FromBody]TaxiOffer Offer)
        {
            db.Offers.Add(Offer);
            await db.SaveChangesAsync();
            return Json(Offer.Place,new JsonSerializerSettings{
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        /*
        public async Task<IActionResult> MyOffers()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            List<TaxiOffer> allOrders = await db.TaxiOffers.ToListAsync();
            List<TaxiOffer> myOffers = new List<TaxiOffer>();
            foreach (var order in allOrders)
            {
                if (order.OfferOwnerId == userId)
                {
                    myOffers.Add(order);
                }
            }
            return View(myOffers);
        }

        public async Task<IActionResult> DeleteTaxiOffer(int id)
        {
            if (id != null)
            {
                TaxiOffer taxiOffer = await db.TaxiOffers.FirstOrDefaultAsync(p => p.Id == id);
                if (taxiOffer != null)
                    return View(taxiOffer);
            }
            return NotFound();
        }*/

        [HttpGet("[action]")]
        [HttpGet("{index}")]
        public async Task<IActionResult> RemoveTaxiOffer(int? index)
        {
            List<TaxiOffer> taxi = await db.Offers.ToListAsync();
            if (index != null)
            {
                TaxiOffer offer = await db.Offers.FirstOrDefaultAsync(p => p.Id == index);
                if (offer != null)
                {
                    db.Offers.Remove(offer);
                    await db.SaveChangesAsync();
                    taxi = await db.Offers.ToListAsync();
                    for (int i = 0; i < taxi.Count; i++)
                    {
                        if(taxi[i].Place.IndexOf('|')>0){
                            taxi[i].Place = taxi[i].Place.Remove(0, taxi[i].Place.IndexOf('|')+1);
                        }
                    }
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
/*
        public async Task<IActionResult> DetailsTaxiOffer(int? id)
        {
            if (id != null)
            {
                TaxiOffer taxiOffer = await db.TaxiOffers.FirstOrDefaultAsync(p => p.Id == id);
                if (taxiOffer != null)
                    return View(taxiOffer);
            }
            return NotFound();
        }
*//*/
        [HttpGet("[action]")]
        [HttpGet("{id}")]
        public async Task<IActionResult> EditTaxiOffer(int? id)
        {
            if (id != null)
            {
                TaxiOffer taxiOffer = await db.Offers.FirstOrDefaultAsync(p => p.Id == id);
                taxiOffer.Place = taxiOffer.Place.Remove(0,taxiOffer.Place.IndexOf('|')+1);
                if (taxiOffer != null)
                    return Json(taxiOffer,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
            return NotFound();
        }

    [HttpPost("[action]")]
        public async Task<IActionResult> EditTaxiOffer([FromBody]TaxiOrder taxiOfferNew)
        {
            //TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == taxiOrderNew.Id);
            //taxiOrder = taxiOrderNew;
            db.TaxiOrders.Update(taxiOfferNew);
            
            await db.SaveChangesAsync();
            return Json(taxiOfferNew.StartPoint,new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }*/
   /*     
        public async Task<IActionResult> AgreeTaxiOffer(int? id)
        {
            if (id != null)
            {
                TaxiOffer taxiOffer = await db.TaxiOffers.FirstOrDefaultAsync(p => p.Id == id);
                if (taxiOffer != null)
                {
                    taxiOffer.OfferStatus = "In progress";
                    db.TaxiOffers.Update(taxiOffer);
                    await db.SaveChangesAsync();
                    return View(taxiOffer);
                }
            }
            return NotFound();
        }

        
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDeleteTaxiOffer(int? id)
        {
            if (id != null)
            {
                TaxiOffer taxiOffer = await db.TaxiOffers.FirstOrDefaultAsync(p => p.Id == id);
                if (taxiOffer != null)
                    return View(taxiOffer);
            }
            return NotFound();
        }*/
    }
}
