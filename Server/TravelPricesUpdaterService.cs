using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using CosmosOdyssey.Shared;
using CosmosOdyssey.Server.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace CosmosOdyssey.Server
{
    public class TravelPricesUpdaterService : BackgroundService
    {

        private readonly HttpClient _httpClient;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TravelPricesUpdaterService> _logger;

        private readonly int _maxTravelPrices = 15;

        public bool IsRunning { get; set; }

        public TravelPricesUpdaterService(HttpClient httpClient, ILogger<TravelPricesUpdaterService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _httpClient = httpClient;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _db = scope.ServiceProvider.GetRequiredService<SpaceTravelContext>();
                try
                {
                    _logger.LogInformation($"{nameof(TravelPricesUpdaterService)} starting {nameof(ExecuteAsync)}");
                    IsRunning = true;
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var newTravelPrices = await _httpClient.GetFromJsonAsync<TravelPrices>("https://cosmos-odyssey.azurewebsites.net/api/v1.0/TravelPrices");

                        #if DEBUG
                        var newWaitTime = newTravelPrices.ValidUntil.AddHours(2).AddSeconds(1).TimeOfDay - DateTime.Now.TimeOfDay;
                        #else
                        var newWaitTime = newTravelPrices.ValidUntil.AddSeconds(1).TimeOfDay - DateTime.Now.TimeOfDay;
                        #endif

                        _logger.LogInformation($"{nameof(TravelPricesUpdaterService)} new update time: {newWaitTime}"); ;

                        // if new data is not loaded to the database
                        if (!_db.TravelPrices.Any() || 
                            !_db.TravelPrices.OrderBy(p => p.ValidUntil).LastOrDefault().TravelPricesId.Equals(newTravelPrices.TravelPricesId))
                        {
                            await _db.TravelPrices.AddAsync(newTravelPrices, stoppingToken);
                            await _db.SaveChangesAsync(stoppingToken);
                        }

                        _logger.LogInformation($"{nameof(TravelPricesUpdaterService)} TravelPrices count: {_db.TravelPrices.Count()}");

                        // 
                        if (_db.TravelPrices.Count() == _maxTravelPrices)
                        {
                            DeleteReservations(_db);
                            DeleteTravelPrices(_db);
                            await _db.SaveChangesAsync(stoppingToken);
                        }

                        _logger.LogInformation($"{nameof(TravelPricesUpdaterService)} running {nameof(ExecuteAsync)}");
                        await Task.Delay(newWaitTime);
                    }
                    IsRunning = false;
                    _logger.LogInformation($"{nameof(TravelPricesUpdaterService)} ending {nameof(ExecuteAsync)}");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message, exception);
                }
                finally
                {
                    IsRunning = false;
                }
            }
        }

        private static void DeleteReservations(SpaceTravelContext _db)
        {
            var reservations = _db.Reservations
                                .Where(r => r.TravelPriceId.Equals(_db.TravelPrices.OrderBy(p => p.ValidUntil).FirstOrDefault().TravelPricesId))
                                .ToList();
            foreach (var reservation in reservations)
            {
                _db.Reservations.Remove(reservation);
            }
        }

        private static void DeleteTravelPrices(SpaceTravelContext _db)
        {
            var removePrices = _db.TravelPrices
                                .Include(p => p.Legs)
                                .ThenInclude(p => p.RouteInfo)
                                .ThenInclude(p => p.From)
                                .Include(p => p.Legs)
                                .ThenInclude(p => p.RouteInfo)
                                .ThenInclude(p => p.To)
                                .Include(p => p.Legs)
                                .ThenInclude(p => p.Providers)
                                .ThenInclude(p => p.Company)
                                .ToList().OrderBy(p => p.ValidUntil).FirstOrDefault();

            foreach (var leg in removePrices.Legs)
            {
                foreach (var provider in leg.Providers)
                {
                    _db.Companies.Remove(provider.Company);
                    _db.Providers.Remove(provider);
                }
                _db.Planets.Remove(leg.RouteInfo.From);
                _db.Planets.Remove(leg.RouteInfo.To);
                _db.RouteInfos.Remove(leg.RouteInfo);
                _db.Legs.Remove(leg);

            }
            _db.TravelPrices.Remove(removePrices);
        }
    }
}
