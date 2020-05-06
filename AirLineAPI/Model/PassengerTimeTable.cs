using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class PassengerTimeTable
    {
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public int TimeTableId { get; set; }
        public TimeTable TimeTable { get; set; }
    }
}
