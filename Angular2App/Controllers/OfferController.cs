﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Angular2App.Data;
using Angular2App.Models;
using Microsoft.AspNetCore.Identity;
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
            return Json(_offers);
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
*/


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
        }

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
