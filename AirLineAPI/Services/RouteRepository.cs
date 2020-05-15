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
            _logger.LogInformation($"Getting passenger by id: {routeID}");
            IQueryable<Route> query = _context.Routes;
            query = query.Where(x => x.ID == routeID)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Route[]> GetRoutes()
        {
            _logger.LogInformation("Getting all routes");
            IQueryable<Route> query = _context.Routes
                .Include(r => r.StartDestination);

            query = query.OrderBy(s => s.StartDestination);
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
        public Task<Route[]> GetRoutesBetweenTimes(int firsthours, int firstminutes, int secondhours, int secoundminutes)
        {
            var ftime = new TimeSpan(0, firsthours, firstminutes, 0);
            var stime = new TimeSpan(0, secondhours, secoundminutes, 0);

            _logger.LogInformation($"Getting all routes between {firsthours}h:{firstminutes}m{secondhours}h:{secoundminutes}");
            IQueryable<Route> query = _context.Routes
                .Where(t => t.TravelTime >= ftime && t.TravelTime <= stime)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);
            return null;
        }

        public async Task<Route[]> GetRoutesByTimeGreatherThan(int hours, int minutes)
        {
            var timeToFind = new TimeSpan(0, hours, minutes, 0);
            _logger.LogInformation($"Getting routes by flight time greater than : {hours}:{minutes}");
            IQueryable<Route> query = _context.Routes
                .Where(t => t.TravelTime >= timeToFind)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination)
                .OrderBy(t => t.TravelTime);

            return await query.ToArrayAsync();
        }

        public async Task<Route[]> GetRoutesByTimeLessThan(int hours, int minutes)
        {
            var timeToFind = new TimeSpan(0, hours, minutes, 0);
            _logger.LogInformation($"Getting routes by flight time less than: {hours}: {minutes}");
            IQueryable<Route> query = _context.Routes
                .Where(t => t.TravelTime <= timeToFind)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination)
                .OrderBy(t => t.TravelTime);

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
    }
}
