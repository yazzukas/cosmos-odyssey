using Microsoft.AspNetCore.Mvc;
using System;
using CosmosOdyssey.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CosmosOdyssey.Server.Data;

namespace CosmosOdyssey.Server.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly ReservationContext _db;


        public ReservationController(ReservationContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetOrders()
        {
            var orders = await _db.Reservations.ToListAsync();
            return orders;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PlaceOrder(Reservation order)
        {
            
            Console.WriteLine("tere");
            order.CreatedTime = DateTime.Now;          

            await _db.Reservations.AddAsync(order);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
