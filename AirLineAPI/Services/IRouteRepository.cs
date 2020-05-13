using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface IRouteRepository : IRepository
    {
        Task<Route[]> GetRoutes();
        Task<Route> GetRouteByID(long RouteID);
        Task<Route[]> GetRoutesByTimeIntervalLessThan(int time);
        Task<Route[]> GetRoutesByTimeIntervalGreatherThan(int time);

    }
}
