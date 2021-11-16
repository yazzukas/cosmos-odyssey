using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CosmosOdyssey.Shared;
using CosmosOdyssey.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Server.Controllers
{
    [Route("api/travelprices")]
    [ApiController]
    public class TravelPricesController : Controller
    {
        private readonly SpaceTravelContext _db;

        public TravelPricesController(SpaceTravelContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public async Task<TravelPrices> GetTravelPrices()
        {
            await using var context = _db;
            return context.TravelPrices
                .Include(p => p.Legs)
                .ThenInclude(p => p.RouteInfo)
                .ThenInclude(p => p.From)
                .Include(p => p.Legs)
                .ThenInclude(p => p.RouteInfo)
                .ThenInclude(p => p.To)
                .Include(p => p.Legs)
                .ThenInclude(p => p.Providers)
                .ThenInclude(p => p.Company)
                .ToList().OrderBy(p => p.ValidUntil).LastOrDefault();
        }

        [HttpGet("lasttravelpriceid")]
        public async Task<string> GetLastTravelPriceId()
        {
            await using var context = _db;
            return context.TravelPrices.OrderBy(p => p.ValidUntil).LastOrDefault().TravelPricesId;

        }
    }
}
