using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosOdyssey.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using CosmosOdyssey.Client;
using CosmosOdyssey.Client.Shared;
using System.Text.Json;
using CosmosOdyssey.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Server.Controllers
{
    [Route("api/travelprices")]
    [ApiController]
    public class TravelPricesController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly ReservationContext _db;

        public TravelPricesController(HttpClient httpClient, ReservationContext db)
        {
            _httpClient = httpClient;
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
            // need to use OnModelCreating
            //return context.TravelPrices.Include(p => p.Legs).ToList().Last();
        }

        [HttpGet("lasttravelpriceid")]
        public async Task<string> GetLastTravelPriceId()
        {
            await using var context = _db;
            return context.TravelPrices.OrderBy(p => p.ValidUntil).LastOrDefault().TravelPricesId;
            // need to use OnModelCreating
            //return context.TravelPrices.Include(p => p.Legs).ToList().Last();
        }

        [HttpGet("1")]
        public async Task<TravelPrices> GetTravelPricesOld()
        {
            return await _httpClient.GetFromJsonAsync<TravelPrices>("https://cosmos-odyssey.azurewebsites.net/api/v1.0/TravelPrices");
        }
    }
}
