using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class RouteRepository : Repository, IRouteRepository
    {

        public RouteRepository(AirLineContext context, ILogger<RouteRepository> logger) : base(context, logger)
        {

        }

        public async Task<Route> GetRouteByID(long routeID)
        {
            _logger.LogInformation($"Getting Route by id: {routeID}");
            IQueryable<Route> query = _context.Routes;
            query = query.Where(x => x.Id == routeID)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Route[]> GetRoutes(int minTime, int maxTime)
        {
            _logger.LogInformation("Getting all routes");
            IQueryable<Route> query = _context.Routes
                .Include(r => r.StartDestination);

            query = query.OrderBy(s => s.StartDestination);
            query = GetRoutesBetweenTimes(minTime, maxTime, query);
            return await query.ToArrayAsync();
        }

        public async Task<Route[]> GetRoutesByEndCity(string city)
        {
            _logger.LogInformation($"Getting routes by enddestination: {city}");
            IQueryable<Route> query = _context.Routes
                .Where(s => s.EndDestination.City == city)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);
            return await query.ToArrayAsync();
        }

        public async Task<Route[]> GetRoutesByStartCity(string city)
        {
            _logger.LogInformation($"Getting routes by startdestination: {city}");
            IQueryable<Route> query = _context.Routes
                .Where(s => s.StartDestination.City == city)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);
            return await query.ToArrayAsync();
        }
       

        public async Task<Route[]> GetRoutesByEndCountry(string country)
        {
            _logger.LogInformation($"Getting route where end destination are equal to {country}");
            IQueryable<Route> query = _context.Routes
                .Where(c => c.EndDestination.Country == country)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);

            return await query.ToArrayAsync();
        }

        public async Task<Route[]> GetRoutesByStartCountry(string country, double minutes)
        {
            IQueryable<Route> query = _context.Routes;
            if (minutes > 0)
            {
                TimeSpan maxTime = new TimeSpan();
                maxTime += TimeSpan.FromMinutes(minutes);
                _logger.LogInformation($"Getting route where start destination are equal to {country} and max travel time {maxTime}");
                query = _context.Routes
               .Where(c => c.StartDestination.Country == country && c.TravelTime <= maxTime)
               .Include(s => s.StartDestination)
               .Include(e => e.EndDestination);
            }
            else
            {
                _logger.LogInformation($"Getting route where start destination are equal to {country}");
                query = _context.Routes
                .Where(c => c.StartDestination.Country == country)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);
            }

            return await query.ToArrayAsync();
        }

        private IQueryable<Route> GetRoutesBetweenTimes(int minMinutes, int maxMinutes, IQueryable<Route> query)
        {
            var minTime = new TimeSpan(0, 0, minMinutes, 0);
            var maxTime = new TimeSpan(0, 0, maxMinutes, 0);
            if (minMinutes > 0 && maxMinutes > 0)
            {
                _logger.LogInformation($"Getting routes with traveltime between {minMinutes} and {maxMinutes} minutes.");
                query = _context.Routes.Where(r => r.TravelTime >= minTime && r.TravelTime <= maxTime)
                    .OrderBy(t => t.TravelTime);
            }

            else if (minMinutes > 0)
            {
                _logger.LogInformation($"Getting routes with traveltime more than {minMinutes} minutes.");
                query = _context.Routes.Where(r => r.TravelTime <= minTime)
                    .OrderBy(t => t.TravelTime);
            }

            else if (maxMinutes > 0)
            {
                _logger.LogInformation($"Getting routes with traveltime less than {maxMinutes} minutes.");
                query = _context.Routes.Where(r => r.TravelTime <= maxTime)
                    .OrderBy(t => t.TravelTime);
            }
           

            _logger.LogInformation($"Get all routes where traveltime are minimum {minTime} and maximum {maxTime}");
            

            return query;
        }
    }
}
