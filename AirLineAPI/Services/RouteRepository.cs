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

        public async Task<Route[]> GetRoutesByEndDestination(string city)
        {
            _logger.LogInformation($"Getting routes by enddestination: {city}");
            IQueryable<Route> query = _context.Routes
                .Where(s => s.EndDestination.City == city)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);
            return await query.ToArrayAsync();
        }

        public async Task<Route[]> GetRoutesByStartDestination(string city)
        {
            _logger.LogInformation($"Getting routes by startdestination: {city}");
            IQueryable<Route> query = _context.Routes
                .Where(s => s.StartDestination.City == city)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);
            return await query.ToArrayAsync();
        }

        public async Task<Route[]> GetRoutesByTimeGreatherThan(int time)
        {
            var timeToFind = new TimeSpan(0, time, 0, 0);
            _logger.LogInformation($"Getting routes by flight time greater than : {time}");
            IQueryable<Route> query = _context.Routes
                .Where(t => t.TravelTime < timeToFind)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);

            return await query.ToArrayAsync();
        }

        public async Task<Route[]> GetRoutesByTimeLessThan(int time)
        {
            var timeToFind = new TimeSpan(0, time, 0, 0);
            _logger.LogInformation($"Getting routes by flight time less than: {time}");
            IQueryable<Route> query = _context.Routes
                .Where(t => t.TravelTime > timeToFind)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);

            return await query.ToArrayAsync();
        }
    }
}
