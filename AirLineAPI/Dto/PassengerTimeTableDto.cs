using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Dto
{
    public class PassengerTimeTableDto
    {
        [Required]
        public long TimeTableID { get; set; }
        [Required]
        public Route Route { get; set; }
        [Required]
        public Flight Flight { get; set; }
        [Required] 
       public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
