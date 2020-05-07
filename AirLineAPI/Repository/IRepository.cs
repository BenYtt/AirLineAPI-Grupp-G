using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Repository
{
    public interface ITimeTableRepository
    {
        Task<List<TimeTable>> GetTimeTableById(int id);
    }
    public interface IDestinationRepository
    {
        Task<List<Destination>> GetDestinationById(int id);
    }
    public interface IRouteRepository
    {
        Task<List<Route>> GetRouteById(int id);
    }
}
