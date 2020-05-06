using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class Passenger
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int PersonalNumber { get; set; }
        public TimeTable TimeTable { get; set; }
        public ICollection<TimeTable> TimeTables { get; set; }
    }
}
