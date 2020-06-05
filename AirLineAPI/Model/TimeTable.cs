using AirLineAPI.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AirLineAPI.Model
{
    public class TimeTable
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public Flight Flight { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public ICollection<PassengerTimeTable> PassengerTimeTables { get; set; }
    }
}