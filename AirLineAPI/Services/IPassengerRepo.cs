using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface IPassengerRepo
    {
        Task<Passenger> GetPassenger(long passengerId, bool includeTimeTable = false);
        Task<Passenger[]> GetPassengers(bool includeTimeTable = false);
    }
}
