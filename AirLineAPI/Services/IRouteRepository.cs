using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    interface IRouteRepository : IRepository
    {
        Task<Route[]> GetFlights();
        Task<Route> GetFlight(long FlightID);
    }
}
