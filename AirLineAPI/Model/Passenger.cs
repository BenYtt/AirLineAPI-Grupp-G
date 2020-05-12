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
        public long IdentificationNumber { get; set; }
        public ICollection<PassengerTimeTable> PassengerTimeTables { get; set; }
    }

}
