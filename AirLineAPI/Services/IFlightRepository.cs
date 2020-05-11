using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    interface ITimeTableRepository : IRepository
    {
        Task<Flight[]> GetFlights();
        Task<Flight> GetFlight(long FlightID);
    }
}
