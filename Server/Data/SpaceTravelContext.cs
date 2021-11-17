using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosOdyssey.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CosmosOdyssey.Server.Data
{
    public class SpaceTravelContext : DbContext
    {
        public SpaceTravelContext(DbContextOptions<SpaceTravelContext> options) : base(options)
        {

        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<TravelPrice> TravelPrices { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Leg> Legs { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<RouteInfo> RouteInfos { get; set; }
    }
}
