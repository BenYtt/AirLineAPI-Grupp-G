using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirLineAPI.Dto
{
    public class PassengerTimeTableDto
    {
        [Required]
        public long PassengerID { get; set; }
        [Required]
        public long TimeTableID { get; set; } 
        public Passenger Passenger { get; set; }
        public TimeTable TimeTable { get; set; }
    }
}
