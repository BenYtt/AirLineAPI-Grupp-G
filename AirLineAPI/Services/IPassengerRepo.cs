using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    interface IPassengerRepo
    {
        Task<Passenger[]> GetPassenger(int passengerId, bool includeTimeTable=false);
    }
}
