using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class PassengerView
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int IdentificationNumber { get; set; }
        public List<TimeTableView> TimeTables { get; set; }
    }
}
