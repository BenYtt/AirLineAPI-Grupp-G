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
    public class FlightRepository : Repository, ITimeTableRepository
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

        public Task<Flight[]> GetFlights()
        {
            throw new NotImplementedException();
        }
    }
}
