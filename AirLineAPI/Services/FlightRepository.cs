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
    public class FlightRepository : Repository, IFlightRepository
    {
        public FlightRepository(AirLineContext airLineContext, ILogger<FlightRepository> logger) : base(airLineContext, logger)
        {

        }

        public async Task<Flight> GetFlightByID(long flightID)
        {
            _logger.LogInformation($"Getting flight from id: {flightID}");
            IQueryable<Flight> query = _context.Flights;

            query = query.Where(f => f.ID == flightID);
            return await query.FirstOrDefaultAsync();
        }

        public Task<Flight[]> GetFlights()
        {
            throw new NotImplementedException();
        }
    }
}
