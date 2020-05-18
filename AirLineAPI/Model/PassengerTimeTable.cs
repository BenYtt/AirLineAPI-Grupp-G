using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
