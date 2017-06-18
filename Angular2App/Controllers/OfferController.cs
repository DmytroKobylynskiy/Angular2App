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
    
    public class OfferController : Controller
    {
        private ApplicationDbContext db;
        private readonly ILogger _logger;
        public OfferController(ILoggerFactory loggerFactory,ApplicationDbContext context)
        {
            db = context;
            _logger = loggerFactory.CreateLogger<OfferController>();
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> Offers()
        {
            List<TaxiOffer> offers = await db.Offers.ToListAsync();
            List<TaxiOffer> _offers = offers;
            for (int i = 0; i < _offers.Count; i++)
            {
                if(offers[i].Place.IndexOf('|')>0){
                     _offers[i].Place = offers[i].Place.Remove(0, offers[i].Place.IndexOf('|')+1);
                }
            }
            return Json(_offers);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> OffersCoord(){
            List<TaxiOffer> taxiOffers = await db.Offers.ToListAsync();
            List<TaxiOffer> taxi = taxiOffers;
            for (int i = 0; i < taxi.Count; i++)
            {
                if(taxiOffers[i].Place.IndexOf('|')>0){
                     taxi[i].Place = taxiOffers[i].Place.Substring(0,taxiOffers[i].Place.IndexOf('|'));
                }
            }
            return Json(taxi);
        }

        [HttpGet("[action]")]
        [HttpGet("{index}")]
        public async Task<IActionResult> OfferById(int index){
            if (index != null)
            {
                TaxiOffer taxiOffer = await db.Offers.FirstOrDefaultAsync(p => p.Id == index);
                if (taxiOffer != null)
                {
                    return Json(taxiOffer,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                }
                
            }
            return NotFound();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOffer([FromBody]TaxiOffer Offer)
        {
            db.Offers.Add(Offer);
            await db.SaveChangesAsync();
            return Json(Offer.Place,new JsonSerializerSettings{
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        
        [HttpGet("[action]")]
        [HttpGet("{ownerId}")]
        public async Task<IActionResult> MyOffers(string ownerId)
        {
            List<TaxiOffer> taxi = new List<TaxiOffer>();
            _logger.LogInformation("asdsd"+ ownerId);
            using(db){
                var taxis = from n in db.Offers
                                    where n.OfferOwnerId==ownerId
                                    select n;
                foreach (var notification in taxis)
                {
                        taxi.Add(notification);
                }
                _logger.LogInformation("asdsd"+ taxi.Count);
                for (int i = 0; i < taxi.Count; i++)
                {
                    if(taxi[i].Place.IndexOf('|')>0){
                        taxi[i].Place = taxi[i].Place.Remove(0, taxi[i].Place.IndexOf('|')+1);
                    }
                }
            }
            return Json(taxi);
        }

        [HttpGet("[action]")]
        [HttpGet("{offerId}")]
        public async Task<IActionResult> RemoveTaxiOffer(int? offerId)
        {
            List<TaxiOffer> taxi = await db.Offers.ToListAsync();
            if (offerId != null)
            {
                TaxiOffer offer = await db.Offers.FirstOrDefaultAsync(p => p.Id == offerId);
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
            return Json(taxi,new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpGet("[action]")]
        [HttpGet("{receiverId}")]
        public async Task<IActionResult> GetTaxiOfferByOwner(string receiverId)
        {
            _logger.LogInformation("2asd13221adfdsfsdfsdfsdf"+receiverId);
            if (receiverId != null)
            {
                _logger.LogInformation("2"+receiverId);
                TaxiOffer taxiOffer = await db.Offers.FirstOrDefaultAsync(p => p.OfferOwnerId == receiverId);
                taxiOffer.Place = taxiOffer.Place.Remove(0,taxiOffer.Place.IndexOf('|')+1);
                _logger.LogInformation(taxiOffer.Place);
                    return Json(taxiOffer,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        [HttpGet("{editedOfferId}")]
        public async Task<IActionResult> EditTaxiOffer(int? editedOfferId)
        {
            if (editedOfferId != null)
            {
                TaxiOffer taxiOffer = await db.Offers.FirstOrDefaultAsync(p => p.Id == editedOfferId);
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
        public async Task<IActionResult> EditTaxiOffer([FromBody]TaxiOffer taxiOfferNew)
        {
            //TaxiOrder taxiOrder = await db.TaxiOrders.FirstOrDefaultAsync(p => p.Id == taxiOrderNew.Id);
            //taxiOrder = taxiOrderNew;
            db.Offers.Update(taxiOfferNew);
            
            await db.SaveChangesAsync();
            return Json(taxiOfferNew,new JsonSerializerSettings {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        [HttpGet("[action]")]
        [HttpGet("{editedTaxiOfferId}")]
        [HttpGet("{date}")]
        [HttpGet("{time}")]
        public async Task<IActionResult> BusyTaxiOffer(string editedTaxiOfferId,string date, string time)
        {
            List<TaxiOffer> taxi = new List<TaxiOffer>();
            if (editedTaxiOfferId != null)
            {
                using(db){
                    var taxis = from n in db.Offers
                                        where n.OfferOwnerId==editedTaxiOfferId
                                        select n;
                    foreach (var notification in taxis)
                    {
                            taxi.Add(notification);
                    }
                    foreach(var item in taxi){
                        item.BusyDate = date;
                        item.BusyTime = time;
                        db.Offers.Update(item);
                    }
                    await db.SaveChangesAsync();
                    return Json(taxi,new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                }
            }
            return NotFound();
        }

    }
}
