using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class DestinationView
    {
        public long ID { get; set; }
        public string FlightName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
