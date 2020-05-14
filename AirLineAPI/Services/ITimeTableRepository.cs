using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface ITimeTableRepository : IRepository
    {
        Task<TimeTable[]> GetTimeTables(bool includePassengers = false, bool includeRoutes = false);
        Task<TimeTable> GetTimeTableByID(long timeTableID, bool includePassengers = false, bool includeRoutes = false);
        Task<TimeTable[]> GetTimeTableByStartDestination(string startDestination, bool includePassengers = false, bool includeRoutes = false);
        Task<TimeTable[]> GetTimeTableByEndDestination(string endDestination, bool includePassengers = false, bool includeRoutes = false);
        Task<TimeTable[]> GetTimeTablesByIntervalLessThan( int hours = 0, int minutes = 0, bool includePassengers = false, bool includeRoutes = false);
        Task<TimeTable[]> GetTimeTablesByIntervalGreaterThan(TimeSpan minTime, bool includePassengers = false, bool includeRoutes = false);
    }
}
