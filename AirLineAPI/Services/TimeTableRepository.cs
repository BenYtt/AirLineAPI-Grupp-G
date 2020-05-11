using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class TimeTableRepository : Repository, ITimeTableRepository
    {
        public TimeTableRepository(AirLineContext airLineContext, ILogger<FlightRepository> logger) : base(airLineContext, logger)
        {

        }

        public Task<Flight> GetFlight(long FlightID)
        {
            throw new NotImplementedException();
        }

        public Task<Flight[]> GetFlights()
        {
            throw new NotImplementedException();
        }
    }
}
