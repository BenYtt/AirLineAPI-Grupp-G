using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AirLineAPI.Services.Interfaces;

namespace AirLineAPI.Model
{
    public class PassengerTimeTable : IEntity
    {
        public int Id { get; set; }
        public int PassengerID { get; set; }
        public Passenger Passenger { get; set; }
        public int TimeTableID { get; set; }
        public TimeTable TimeTable { get; set; }
    }
}