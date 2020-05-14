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
        private static IQueryable<TimeTable> IncludePassengersAndRoutes(bool includePassengers, bool includeRoutes, IQueryable<TimeTable> query)
        {
            if (includePassengers && includeRoutes)
            {
                query = query.Include(a => a.PassengerTimeTables)
                    .ThenInclude(a => a.Passenger)
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

            return query;
        }

        public async Task<TimeTable[]> GetTimeTables(bool includePassengers = false, bool includeRoutes = false)
        {
            _logger.LogInformation("Getting TimeTables.");
            IQueryable<TimeTable> query = _context.TimeTables;

            query = IncludePassengersAndRoutes(includePassengers, includeRoutes, query);

            return await query.ToArrayAsync();
        }


        public async Task<TimeTable> GetTimeTableByID(long timeTableID, bool includePassengers = false, bool includeRoutes = false)
        {
            _logger.LogInformation($"Getting TimeTable from id: {timeTableID}.");
            IQueryable<TimeTable> query = _context.TimeTables;

            query = query.Where(t => t.ID == timeTableID);
            query = IncludePassengersAndRoutes(includePassengers, includeRoutes, query);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<TimeTable[]> GetTimeTableByStartDestination(string startDestination, bool includePassengers = false, bool includeRoutes = false)
        {
            _logger.LogInformation($"Getting TimeTable with StartDestination: {startDestination}.");
            IQueryable<TimeTable> query = _context.TimeTables.Where(a => a.Route.StartDestination.City == startDestination);
            query = IncludePassengersAndRoutes(includePassengers, includeRoutes, query);

            return await query.ToArrayAsync();
        }

        public async Task<TimeTable[]> GetTimeTableByEndDestination(string endDestination, bool includePassengers = false, bool includeRoutes = false)
        {
            _logger.LogInformation($"Getting TimeTable with EndDestination: {endDestination}.");
            IQueryable<TimeTable> query = _context.TimeTables.Where(a => a.Route.EndDestination.City == endDestination);
            query = IncludePassengersAndRoutes(includePassengers, includeRoutes, query);

            return await query.ToArrayAsync();
        }

        public async Task<TimeTable[]> GetTimeTablesByIntervalLessThan(
            int hours = 0, int minutes = 0, bool includePassengers = false, bool includeRoutes = false)
        {
            _logger.LogInformation($"Getting TimeTables With Travel Time Less Than '{hours}' Hours, '{minutes}'.");
            TimeSpan travelTime = new TimeSpan(0, 0, 0, 0);

            try
            {
                travelTime = new TimeSpan(0, hours, minutes, 0);
                _logger.LogInformation($"Converted '{hours}' Hours, '{minutes}' Minutes to {travelTime}.");
            }
            catch (FormatException)
            { 
                _logger.LogInformation("Bad Time Format.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Time Is Out Of Range.");
            }

            IQueryable<TimeTable> query = _context.TimeTables.Where(a => a.Route.TravelTime <= travelTime);
            query = IncludePassengersAndRoutes(includePassengers, includeRoutes, query);

            return await query.ToArrayAsync();
        }

        public async Task<TimeTable[]> GetTimeTablesByIntervalGreaterThan(TimeSpan minTime, bool includePassengers = false, bool includeRoutes = false)
        {
            _logger.LogInformation($"Getting TimeTables With Travel Time Greater Then {minTime}");

            IQueryable<TimeTable> query = _context.TimeTables.Where(t => t.Route.TravelTime >= minTime);
            query = IncludePassengersAndRoutes(includePassengers, includeRoutes, query);
            return await query.ToArrayAsync();
        }
    }
}
