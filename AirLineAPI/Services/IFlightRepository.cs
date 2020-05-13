using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface IFlightRepository : IRepository
    {
        Task<Flight[]> GetFlights();
        Task<Flight> GetFlightByID(long flightID);
        Task<Flight[]> GetFlightsByManufacturer(string manufacturer);
        Task<Flight[]> GetFlightsByModel(string model);
    }
}
