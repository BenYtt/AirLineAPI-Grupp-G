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
        Task<Route[]> GetRoutesByStartCity(string city);
        Task<Route[]> GetRoutesByEndCity(string city);
        Task<Route[]> GetRoutesBetweenTimes(int firsthours, int firstminutes, int secondhours, int secoundminutes);
        Task<Route[]> GetRoutesByEndCountry(string country);
        Task<Route[]> GetRoutesByStartCountry(string country, double maxtime);
        
    }
}
