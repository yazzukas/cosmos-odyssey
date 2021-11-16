using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosOdyssey.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CosmosOdyssey.Server.Data
{
    public class ReservationContext : DbContext
    {

        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
        {

        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<TravelPrices> TravelPrices { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Leg> Legs { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<RouteInfo> RouteInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<Reservation>().ToTable("Reservations");

            /*modelBuilder.Entity<TravelPrices>()
                .HasMany(i => i.Legs)
                .WithOne(c => c.TravelPrices)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Leg>()
                .HasMany(i => i.Providers)
                .WithOne(c => c.Leg)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Provider>()
                .HasOne(i => i.Company)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);*/

            //modelBuilder.Entity<TravelPrices>().OwnsMany(p => p.Legs).;

            /*modelBuilder.Entity<Leg>()
                .OwnsOne(e => e.RouteInfo);*/

            /*modelBuilder.Entity<RouteInfo>()
                .OwnsOne(e => e.From)
                ;

            modelBuilder.Entity<RouteInfo>()
                .OwnsOne(e => e.To);*/

            //modelBuilder.Entity<Company>().ToTable("Companies");
            //modelBuilder.Entity<Planet>().ToTable("Planets");
            /*modelBuilder.Entity<Provider>()
                .OwnsOne(e => e.Company);*/ // yyyeye
            /*modelBuilder.Entity<RouteInfo>()
                .HasKey(k => new { k.LegId, k.RouteId });*/

            //modelBuilder.Entity<Reservation>().HasOne<RouteInfo>();
            //modelBuilder.Entity<RouteInfo>().HasMany<Reservation>();

        }
    }
}
