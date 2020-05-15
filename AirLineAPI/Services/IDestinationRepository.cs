using AirLineAPI.Model;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public interface IDestinationRepository : IRepository
    {
        Task<Destination> GetDestinationByID(long destinationID);

        Task<Destination[]> GetDestinations();

        Task<Destination[]> GetDestinationsByCountry(string country);

        Task<Destination> GetDestinationByCity(string city);
    }
}
