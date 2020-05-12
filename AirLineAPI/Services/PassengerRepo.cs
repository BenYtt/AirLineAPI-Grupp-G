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
    public class PassengerRepo : Repository, IPassengerRepo
    {
        public PassengerRepo(AirLineContext context, ILogger<Repository> logger) : base(context, logger) { }
        public async Task<Passenger[]> GetPassengers(bool includeTimeTable = false)
        {
            _logger.LogInformation($"Getting passengers");
            IQueryable<Passenger> query = _context.Passengers;
            if (includeTimeTable)
            {
                query = query.Include(x => x.PassengerTimeTables);
            }
            return await query.ToArrayAsync();
        
        }
        public async Task<Passenger> GetPassenger(int passengerId, bool includeTimeTable = false)
        {
            _logger.LogInformation($"Getting passenger by id {passengerId}");
            IQueryable<Passenger> query = _context.Passengers;
            if (includeTimeTable)
            {
                query = query.Include(t => t.PassengerTimeTables);
            }
            query = query.Where(x => x.ID == passengerId);
            return await query.FirstOrDefaultAsync();
        }
    }
}
