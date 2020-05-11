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
    public class DestinationRepository : Repository, IDestinationRepository
    {

        public DestinationRepository(AirLineContext airLineContext, ILogger<DestinationRepository> logger): base (airLineContext, logger)
        {
            
        }
        public async Task<Destination> GetDestinationByID(int destinationID)
        {
            _logger.LogInformation($"Getting destination with ID {destinationID}");
            
            IQueryable<Destination> destination = _context.Destinations;

            return await destination.FirstOrDefaultAsync(d => d.ID == destinationID);
        }

        public Task<Destination[]> GetDestinations()
        {
            throw new NotImplementedException();
        }
    }
}
