using AirLineAPI.Dto;
using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Repository
{
    public interface ITimeTableRepository
    {
        Task<TimeTableView> GetTimeTableById(int id);
    }
    public interface IDestinationRepository
    {
        Task<DestinationView> GetDestinationById(int id);
    }
    public interface IRouteRepository
    {
        Task<RouteView> GetRouteById(int id);
    }

    public interface IPassengerRepository
    {
        Task<PassengerView> GetPassengerById(int id);
    }
}
