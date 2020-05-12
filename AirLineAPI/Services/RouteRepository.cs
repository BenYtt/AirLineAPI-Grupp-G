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

        public async Task<Route> GetRoute(long routeID)
        {
            _logger.LogInformation($"Getting passenger by id {routeID}");
            IQueryable<Route> query = _context.Routes;
            query = query.Where(x => x.ID == routeID)
                .Include(s => s.StartDestination)
                .Include(e => e.EndDestination);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Route[]> GetRoutes()
        {
            _logger.LogInformation("Getting events");
            IQueryable<Route> query = _context.Routes
                .Include(r => r.StartDestination); 

            query = query.OrderBy(s => s.StartDestination);
            return await query.ToArrayAsync();
        }
    }
}
