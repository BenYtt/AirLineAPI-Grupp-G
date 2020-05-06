using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class PassengerTimeTable
    {
        public long PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        public long TimeTableId { get; set; }
        public TimeTable TimeTable { get; set; }
    }
}
