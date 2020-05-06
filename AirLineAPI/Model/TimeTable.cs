using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class TimeTable
    {
        public long ID { get; set; }
        public Route Route { get; set; }
        public Flight Flight { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
