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

        Task<Flight> GetFlightById(int id);

        Task<Flight[]> GetFlightsByManufacturer(string manufacturer);

        Task<Flight[]> GetFlightsByModel(string model);
    }
}