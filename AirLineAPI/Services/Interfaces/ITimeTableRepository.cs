using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface ITimeTableRepository : IRepository
    {
        Task<TimeTable[]> GetTimeTables(int minMinutes, int maxMinutes,
                                        bool includePassengers = false, bool includeRoutes = false);

        Task<TimeTable> GetTimeTableById(int id, bool includePassengers = false, bool includeRoutes = false);

        Task<TimeTable[]> GetTimeTableByStartDestination(string startDestination, bool includePassengers = false, bool includeRoutes = false);

        Task<TimeTable[]> GetTimeTableByEndDestination(string endDestination, bool includePassengers = false, bool includeRoutes = false);
    }
}