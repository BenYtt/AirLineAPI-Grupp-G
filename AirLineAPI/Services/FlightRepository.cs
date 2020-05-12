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

        public async Task<Flight> GetFlight(long FlightID)
        {
            //_logger.LogInformation($"Getting Flight with ID {FlightID}.");

            //IQueryable<Flight> flight = _context.Flights;

            //return await flight.SingleOrDefaultAsync(f => f.ID == FlightID);
            throw new NotImplementedException();

        }

        public async Task<Flight[]> GetFlights()
        {
            _logger.LogInformation("Getting flights");
            IQueryable<Flight> query = _context.Flights;

            query = query.OrderBy(f => f.Manufacturer);
            return await query.ToArrayAsync();
        }
    }
}
