using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface ITimeTableRepository : IRepository
    {
        Task<TimeTable[]> GetTimeTables(bool includePassangers = false, bool includeRoutes = false);
        Task<TimeTable> GetTimeTableByID(long timeTableID, bool includePassangers = false, bool includeRoute = false);
    }
}
