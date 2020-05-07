using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class TimeTableView
    {
        public long ID { get; set; }
        public string RouteName { get; set; }
        public string FlightName { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }

    public class DestinationView
    {
        public long ID { get; set; }
        public string FlightName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }

    public class RouteView
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public Destination StartDestination { get; set; }
        public Destination EndDestination { get; set; }
        public TimeSpan TravelTime { get; set; }
    }
}
