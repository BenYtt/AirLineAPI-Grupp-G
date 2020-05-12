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

        public Task<TimeTable[]> GetTimeTables(bool includePassengers = false, bool includeRoutes = false)
        {
            throw new NotImplementedException();
        }

        public async Task<TimeTable> GetTimeTableByID(long timeTableID, bool includePassengers = false, bool includeRoute = false)
        {
            _logger.LogInformation($"Getting TimeTable from id: {timeTableID}");
            IQueryable<TimeTable> query = _context.TimeTables;

            query = query.Where(t => t.ID == timeTableID);
            return await query.FirstOrDefaultAsync();
        }
    }
}
