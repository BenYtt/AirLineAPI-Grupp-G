using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface IDestinationRepository : IRepository
    {
        Task<Destination> GetDestinationByID(int destinationID);

        Task<Destination[]> GetDestinations();
    }
}
