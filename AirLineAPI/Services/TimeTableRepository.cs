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
    public class TimeTableRepository : Repository, ITimeTableRepository
    {
        public TimeTableRepository(AirLineContext airLineContext, ILogger<FlightRepository> logger) : base(airLineContext, logger)
        {

        }

        public async Task<TimeTable[]> GetTimeTables(bool includePassengers = false, bool includeRoutes = false)
        {
            _logger.LogInformation("Getting TimeTables.");
            IQueryable<TimeTable> query = _context.TimeTables;


        public async Task<TimeTable> GetTimeTableByID(long timeTableID, bool includePassengers = false, bool includeRoute = false)
        {
            _logger.LogInformation($"Getting TimeTable from id: {timeTableID}");
            IQueryable<TimeTable> query = _context.TimeTables;
            
            if (includePassengers && includeRoutes)
            {
                query = query.Include(a => a.PassengerTimeTables)
                    .Include(a => a.Route);
            }
            else if (includePassengers)
            {
                query = query.Include(a => a.PassengerTimeTables)
                    .ThenInclude(a => a.Passenger);
            }
            else if (includeRoutes)
            {
                query = query.Include(a => a.Route);
            }
            
            query = query.Where(t => t.ID == timeTableID);
            return await query.FirstOrDefaultAsync();
        }
    }
}
