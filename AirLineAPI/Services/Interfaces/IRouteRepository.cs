using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface IRouteRepository : IRepository
    {
        Task<Route[]> GetRoutes(int minTime, int maxTime);

        Task<Route> GetRouteById(int id);

        Task<Route[]> GetRoutesByStartCity(string city);

        Task<Route[]> GetRoutesByEndCity(string city);

        Task<Route[]> GetRoutesByEndCountry(string country);

        Task<Route[]> GetRoutesByStartCountry(string country, double maxtime);
    }
}