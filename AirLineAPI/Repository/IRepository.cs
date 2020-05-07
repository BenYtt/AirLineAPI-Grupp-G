using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Repository
{
    interface ITimeTableRepository
    {
        Task<List<TimeTable>> GetTimeTableById(int id);
    }
    interface IDestinationRepository
    {
        Task<List<Destination>> GetDestinationById(int id);
    }
    interface IRouteRepository
    {
        Task<List<Route>> GetRouteById(int id);
    }
}
