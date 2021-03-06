﻿using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class DestinationRepository : Repository, IDestinationRepository
    {
        public DestinationRepository(AirLineContext airLineContext, ILogger<DestinationRepository> logger) : base(airLineContext, logger)
        {
        }

        public async Task<Destination[]> GetDestinations()
        {
            _logger.LogInformation($"Getting destinations");

            IQueryable<Destination> destinations = _context.Destinations;

            return await destinations.ToArrayAsync();
        }

        public async Task<Destination> GetDestinationById(int id)
        {
            _logger.LogInformation($"Getting destination with ID {id}");

            IQueryable<Destination> destination = _context.Destinations;

            return await destination.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Destination> GetDestinationByCity(string city)
        {
            _logger.LogInformation($"Getting destination with ID {city}");

            IQueryable<Destination> destination = _context.Destinations;

            return await destination.FirstOrDefaultAsync(d => d.City == city);
        }

        public async Task<Destination[]> GetDestinationsByCountry(string country)
        {
            _logger.LogInformation($"Getting gestinations by country {country}");

            IQueryable<Destination> destinations = _context.Destinations;

            return await destinations.Where(d => d.Country == country).ToArrayAsync();
        }
    }
}