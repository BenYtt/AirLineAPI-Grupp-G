using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class FlightRepository : Repository, IFlightRepository
    {
        public FlightRepository(AirLineContext airLineContext, ILogger<FlightRepository> logger) : base(airLineContext, logger)
        {
        }

        public async Task<Flight[]> GetFlights()
        {
            _logger.LogInformation("Getting flights");

            IQueryable<Flight> query = _context.Flights;
            query = query.OrderBy(f => f.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Flight> GetFlightById(int id)
        {
            _logger.LogInformation($"Getting flight from id: {id}");

            IQueryable<Flight> query = _context.Flights.Where(f => f.Id == id);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<Flight[]> GetFlightsByManufacturer(string manufacturer)
        {
            _logger.LogInformation($"Getting flights made by {manufacturer}.");

            IQueryable<Flight> query = _context.Flights.Where(f => f.Manufacturer == manufacturer);
            query = query.OrderBy(f => f.Manufacturer);

            return await query.ToArrayAsync();
        }

        public async Task<Flight[]> GetFlightsByModel(string model)
        {
            _logger.LogInformation($"Getting flights with model {model}.");

            IQueryable<Flight> query = _context.Flights.Where(f => f.Model == model);
            query = query.OrderBy(f => f.Model);

            return await query.ToArrayAsync();
        }
    }
}