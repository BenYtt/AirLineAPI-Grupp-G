using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface IPassengerRepository : IRepository
    {
        Task<Passenger> GetPassengerById(long passengerId, bool includeTimeTable = false);
        Task<Passenger[]> GetPassengers(bool includeTimeTable = false);
        Task<Passenger> GetPassengerByName(string name);
        Task<Passenger> GetPassengerByIdentificationNumber(long identitificationNm);
    }
}
