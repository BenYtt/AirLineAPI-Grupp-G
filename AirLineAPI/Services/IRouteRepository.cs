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
        Task<Route[]> GetRoutesByTimeLessThan(int hours, int minutes);
        Task<Route[]> GetRoutesByTimeGreatherThan(int hours, int minutes);
        Task<Route[]> GetRoutesByStartDestination(string city);
        Task<Route[]> GetRoutesByEndDestination(string city);
        Task<Route[]> GetRoutesBetweenTimes(int firsthours, int firstminutes, int secondhours, int secoundminutes);
    }
}
