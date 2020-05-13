using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class TimeTable
    {
        public long ID { get; set; }
        [Required]
        public Route Route { get; set; }
        [Required]
        public Flight Flight { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public ICollection<PassengerTimeTable> PassengerTimeTables { get; set; }
    }
}
