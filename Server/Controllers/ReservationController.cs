using Microsoft.AspNetCore.Mvc;
using System;
using CosmosOdyssey.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CosmosOdyssey.Server.Data;

namespace CosmosOdyssey.Server.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly SpaceTravelContext _db;

        public ReservationController(SpaceTravelContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetReservations()
        {
            var orders = await _db.Reservations.ToListAsync();
            return orders;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddReservation(Reservation reservation)
        {
            if(reservation != null)
            {
                reservation.CreatedTime = DateTime.Now;

                await _db.Reservations.AddAsync(reservation);
                await _db.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
